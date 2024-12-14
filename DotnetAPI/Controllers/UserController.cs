using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public UserController(){

        }

        [HttpGet("test")]
        public string[] Test()
        {
            string[] responseAttay = new string[]{
                "test1",
                "test2",
            };

            return responseAttay;
        }
    }
}
