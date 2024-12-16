using Newtonsoft.Json;
using TodoWebAPI.Models;

namespace TodoWebAPI.Classes
{
    public class TodoService
    {
        public static string retrieveTodos(List<Todo> todos)
        {
            Response response = new Response();

            if (todos.Any()) { 
                response.StatusCode = 200;

                var returnObj = new {todos, response};

                return JsonConvert.SerializeObject(returnObj);
            }

            response.StatusCode = 100;
            response.ErrorMessage = "No data found.";

            return JsonConvert.SerializeObject(response);

        }

        public static string retrieveTodo(int id, List<Todo> todos)
        {
            Response response = new Response();

            if (todos.Where(todo => todo.id == id).Any()) { 
                response.StatusCode = 200;
                var todo = todos.Where(todo => todo.id == id).FirstOrDefault();

                var returnObj = new { todo, response };

                return JsonConvert.SerializeObject(returnObj);
            }

            response.StatusCode = 100;
            response.ErrorMessage = "No data found.";

            return JsonConvert.SerializeObject(response);
        }

        public static string createTodo(string description)
        {
            var response = new Response();

            var todo = new Todo();

            todo.description = description;
            response.StatusCode = 200;

            var returnObj = new { todo, response };

            return JsonConvert.SerializeObject(returnObj);
        }

        public static string editTodo(int id , string description, List<Todo> todos)
        {
            var response = new Response();

            var todo = new Todo();

            if (todos.Where(todo => todo.id == id).Any())
            {
                todo = todos.Where(todo => todo.id == id).FirstOrDefault();

                todo.description = description;
                response.StatusCode = 200;

                var returnObj = new { todo, response };

                return JsonConvert.SerializeObject(returnObj);
            }

            response.StatusCode = 100;
            response.ErrorMessage = "No data found.";

            return JsonConvert.SerializeObject(response);
        }

        public static string deleteTodo(int id, List<Todo> todos)
        {
            var response = new Response();

            var todo = new Todo();

            if (todos.Where(todo => todo.id == id).Any())
            {
                todo = todos.Where(todo => todo.id == id).FirstOrDefault();

                response.StatusCode = 200;

                var returnObj = new { todo, response };

                return JsonConvert.SerializeObject(returnObj);
            }

            response.StatusCode = 100;
            response.ErrorMessage = "No data found.";

            return JsonConvert.SerializeObject(response);
        }

        public static string DeleteAllTodos(List<Todo> todos)
        {
            var response = new Response();

            var todo = new Todo();

            if (todos.Count > 0)
            {
                todos.Clear();

                response.StatusCode = 200;

                return JsonConvert.SerializeObject(response);
            }

            response.StatusCode = 100;
            response.ErrorMessage = "No data found.";

            return JsonConvert.SerializeObject(response);
        }
    }
}
