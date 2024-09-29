using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FinalProlectWeb.API
{
    [Route("API/Board")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        //Create new Board
        [HttpGet("createboard/{username}/{namefile}/{size}")]//return new partial
        public IActionResult createboard(string username, string namefile, int size)
        {
            Board.CreateNewBoard(username, namefile, size);
            Board b = new Board();
            b.UserName = username;
            b.FileName = namefile;
            b.Size = size;
            var res = new PartialViewResult()
            {
                ViewName = "_partialboard",
                ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = b
                }
            };
            return res;
        }

        [HttpGet("retpartial/{username}/{degel}/{size}")]//return partial board
        public IActionResult retpartial(string username,bool degel,int size)
        {
            Board b = new Board();
            b.UserName = username;
            b.Size = size;
            b.IsDisplay = degel;
            var res = new PartialViewResult()
            {
                ViewName = "_partialboard",
                ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = b
                }
            };
            return res;
        }

        [HttpGet("createboardi/{username}/{namefile}/{size}")]//create new board without return partial
        public int createboardi(string username, string namefile, int size)
        {
            int x = Board.CreateNewBoard(username, namefile, size);
            return x;
        }

        [HttpPost("shmorboard")]//save board
        public int shmorboard([FromBody] Board b)
        {
            int x = Board.Shmor(b);
            return x;
        }

        [HttpGet("deletedrow/{username}/{namefile}")]//delete board
        public int deletedrow(string username, string namefile)
        {
            int x = Board.DeleteBoard(username, namefile);
            return x;
        }

        //change board size
        [HttpGet("changesize/{username}/{filename}/{size}")]//return partial board
        public IActionResult changesize(string username, string filename, int size)
        {
            Board.DeleteBoard(username, filename);
            Board b = new Board();
            b.UserName = username;
            b.FileName = filename;
            b.Size = size;
            Board.Writee(b);
            var res = new PartialViewResult()
            {
                ViewName = "_partialboard",
                ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = b
                }

            };
            return res;
        }

        //open saved text and return partial board to edit 
        [HttpGet("opentext/{username}/{filename}")]
        public IActionResult opentext(string username, string filename)
        {
            var res = new PartialViewResult()
            {
                ViewName = "_partialboard",
                ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = Board.OpenBoard(username, filename, false)
                }

            };
            return res;
        }

    }
}