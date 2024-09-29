using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace FinalProlectWeb.API
{
    [Route("API/Users")]
    [ApiController]
    public class UsersController:ControllerBase
    {
        [HttpGet("createuser/{name}/{pass}")]
        public int createuser(string name, string pass)//add new user
        {
            int x = 0;
            x = Users.CreateNewUser(name, pass);
            return x;
        }

        [HttpGet("checkuser/{name}/{pass}")]
        public bool checkuser(string name, string pass)//check user exists
        {
            bool degel = true;
            degel = Users.Check(name, pass);
            return degel;
        }

        [HttpGet("checkmanager/{name}/{code}")]
        public bool checkmanager(string name, int code)//check manager
        {

            return Users.CheckManager(name, code);

        }

        [HttpGet("PartialUsers")]
        public IActionResult PartialUsers()//return users partial
        {
            List<Users> l = Users.ReturnUsers();
            var res = new PartialViewResult()
            {
                ViewName = "_partialusers",
                ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = l
                }
            };
            return res;
        }

        //delete user
        [HttpPost("deleteuser")]
        public int deleteuser([FromBody] Users u)
        {
            int x = Users.DeleteUser(u.UserName, Users.ManagerName, Users.ManagerCode);
            return x;
        }
    }
}