using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using TodoWebAPI.Models;

namespace TodoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        public readonly IConfiguration _configuration;
        public TodoController(IConfiguration configuration)
        {
            _configuration = configuration;  
        }

        [HttpGet]
        [Route("GetAllTodos")]
        public string GetTodos()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("TodoAppCon"));
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM todos", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Response response = new Response();

            List<Todo> todoList = new List<Todo>();

            if (dt.Rows.Count > 0 )
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Todo todo = new Todo();

                    todo.id = Convert.ToInt32(dt.Rows[i]["id"]);
                    todo.description = dt.Rows[i]["description"].ToString() ?? "";

                    todoList.Add(todo);
                }
            }

            if (todoList.Count > 0)
            {
                return JsonConvert.SerializeObject(todoList);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No Data Found.";

                return JsonConvert.SerializeObject(response);
            }
        }
        [HttpGet]
        [Route("GetTodo")]
        public string GetTodo(int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("TodoAppCon"));
            SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM todos WHERE id = {id}", con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            Response response = new Response();

            Todo? todo = null;

            if (dt.Rows.Count > 0)
            {
                todo = new Todo();
                todo.id = Convert.ToInt32(dt.Rows[0]["id"]);
                todo.description = dt.Rows[0]["description"].ToString() ?? "";
            }

            if (todo != null)
            {
                return JsonConvert.SerializeObject(todo);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No Data Found.";

                return JsonConvert.SerializeObject(response);
            }

        }

        [HttpPost]
        [Route("CreateTodo")]
        public string CreateTodo(string description)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("TodoAppCon"));
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM todos WHERE 0 = 1 ", con);

                var dt = new DataTable();
                da.Fill(dt);

                var newRow = dt.NewRow();

                newRow["description"] = description;

                dt.Rows.Add(newRow);

                new SqlCommandBuilder(da);
                da.Update(dt);
             
                //Todo? todo = null;

                //todo = da.RowUpdated["id"];             
                //todo.description = newRow["description"].ToString() ?? "";

                return JsonConvert.SerializeObject(newRow);
            }
            catch (Exception ex)
            {
                Response response = new Response();

                response.StatusCode = 100;
                response.ErrorMessage = ex.Message;

                return JsonConvert.SerializeObject(response);
            }
   

        }

        [HttpPut]
        [Route("EditTodo")]
        public string UpdateTodo(int id, string description)
        {
            Response response = new Response();
            Todo? todo = null;

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("TodoAppCon")))
            {
                // Use parameterized query for the update
                string query = "UPDATE todos SET description = @description WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@id", id);

                    try
                    {
                        con.Open();

                        // Execute the query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // If the update is successful, retrieve the updated Todo
                            todo = new Todo();
                            todo.id = id;
                            todo.description = description;
                        }
                    }
                    catch (Exception ex)
                    {
                        response.StatusCode = 500;
                        response.ErrorMessage = "An error occurred: " + ex.Message;
                        return JsonConvert.SerializeObject(response);
                    }
                }
            }

            if (todo != null)
            {
                // Return the updated todo as JSON
                return JsonConvert.SerializeObject(todo);
            }
            else
            {
                // Return a response indicating no data was found or updated
                response.StatusCode = 100;
                response.ErrorMessage = "No Data Found.";
                return JsonConvert.SerializeObject(response);
            }
        }

        [HttpDelete]
        [Route("DeleteTodo")]
        public string DeleteTodo(int id)
        {
            Response response = new Response();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("TodoAppCon")))
            {
                // Use parameterized query for the update
                string query = "DELETE FROM todos WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@id", id);

                    try
                    {
                        con.Open();

                        response.ErrorMessage = "Todo deleted";
                        response.StatusCode = 200;

                        return JsonConvert.SerializeObject(response);
                    }
                    catch (Exception ex)
                    {
                        response.StatusCode = 500;
                        response.ErrorMessage = "An error occurred: " + ex.Message;
                        return JsonConvert.SerializeObject(response);
                    }
                }
            }
            
        }

    }
}
