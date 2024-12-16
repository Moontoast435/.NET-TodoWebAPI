using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using TodoWebAPI.Classes;
using TodoWebAPI.Models;

namespace TodoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        private readonly LHQ_SeanContext _context;

        public TodoController(LHQ_SeanContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllTodos")]
        public string GetTodos()
        {
            try
            {
                var todos = _context.Todos.ToList();

                return TodoService.retrieveTodos(todos);
            }
            catch (Exception ex)
            {
                var error = "Fail to get todos: " + ex.Message;
                Console.WriteLine(error);
                throw;
            }   
        }


        [HttpGet]
        [Route("GetTodo")]
        public string GetTodo(int id)
        {
            try
            {
                var todos = _context.Todos.ToList();

                return TodoService.retrieveTodo(id, todos);
            }
            catch (Exception ex)
            {
                var error = "Fail to get todo: " + ex.Message;
                Console.WriteLine(error);
                throw;
            }
        }

        [HttpPost]
        [Route("CreateTodo")]
        public string CreateTodo(string description)
        {
            try {
                var res = TodoService.createTodo(description);

                var createdTodo = JsonConvert.DeserializeObject<CreateTodoResponse>(res);

                var nR = new Todo()
                {
                    description = createdTodo.Todo.description != null ? createdTodo.Todo.description : "No description entered."
                };

                _context.Todos.Add(nR); 

                _context.SaveChanges();

                createdTodo.Todo.id = nR.id;

                res = JsonConvert.SerializeObject(createdTodo);

                return res;

            }
            catch (Exception ex)
            {
                var error = "Fail to create todo: " + ex.Message;
                Console.WriteLine(error);
                throw;
            }
        }

        [HttpPut]
        [Route("EditTodo")]
        public string UpdateTodo(int id, string description)
        {
            try
            {
                var todos = _context.Todos.ToList();

                var res = TodoService.editTodo(id, description, todos);

                var editedTodo = JsonConvert.DeserializeObject<CreateTodoResponse>(res);

                if (editedTodo.Response.StatusCode == 200)
                {
                    _context.Todos.Where(todo => todo.id == id).First().description = description;

                    _context.SaveChanges();

                    return res;
                }
                
                return res;
            }
            catch (Exception ex)
            {
                var error = "Fail to edit todo: " + ex.Message;
                Console.WriteLine(error);
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteTodo")]
        public string DeleteTodo(int id)
        {
            try
            {
                var todos = _context.Todos.ToList();

                var res = TodoService.deleteTodo(id, todos);

                var deletedTodo = JsonConvert.DeserializeObject<CreateTodoResponse>(res);

            if (deletedTodo.Response.StatusCode == 200)
            {
                _context.Todos.Remove(_context.Todos.Where(todo => todo.id == id).First());

                _context.SaveChanges();

                return res;
            }

            return res;
            }
            catch (Exception ex)
            {
                var error = "Fail to delete todo: " + ex.Message;
                Console.WriteLine(error);
                throw;
            }

        }

        [HttpDelete]
        [Route("DeleteAllTodos")]
        public string DeleteAllTodos()
        {
            try
            {
                var res = JsonConvert.DeserializeObject<Response>(TodoService.DeleteAllTodos(_context.Todos.ToList()));

                if (res.StatusCode == 200)
                {
                    var toRemove = _context.Todos.ToList();
                    _context.Todos.RemoveRange(toRemove);

                    _context.SaveChanges();

                    return JsonConvert.SerializeObject(res);
                }

                return JsonConvert.SerializeObject(res);
            }
            catch (Exception ex)
            {
                var error = "Fail to delete all todos: " + ex.Message;
                Console.WriteLine(error);
                throw;
            }
        }

    }
}
