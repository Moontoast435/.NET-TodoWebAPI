using Microsoft.AspNetCore.Mvc;
using TodoWebAPI.Models;
using TodoWebAPI.Classes;
using Newtonsoft.Json;

namespace TodoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly LHQ_SeanContext _context;

        public AccountController(LHQ_SeanContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("CreateAccount")]
        public string CreateAccount(string userIdx, string userName, string passwordHash)
        {
            try
            {
                CreateAccountResponse responseObj = new();

                var doesAccountExist = AccountService.CheckIfAccountExists(_context.Users.ToList(), userName);

                if (doesAccountExist)
                {
                    responseObj.StatusCode = 409;
                    responseObj.Message = "Account already exists.";
                    return JsonConvert.SerializeObject(responseObj);
                }

                User newUser = new();

                newUser.GUserIdx = Guid.Parse(userIdx);
                newUser.SUsername = userName;
                newUser.SPasswordHash = passwordHash;

                _context.Users.Add(newUser);

                _context.SaveChanges();

                responseObj.StatusCode = 200;
                responseObj.Message = "Account created.";

                return JsonConvert.SerializeObject(responseObj);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to create account: " + ex.Message);
                throw;
            }

        }
    }
}
