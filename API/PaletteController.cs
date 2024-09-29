using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace FinalProlectWeb.API
{
    [Route("API/Palette")]
    [ApiController]
    public class PaletteController:ControllerBase
    {
        [HttpGet("createpalette/{username}/{listcolors}")]//create pallete
        public bool createpalette(string username, List<string> listcolors)
        {
            bool x = Palette.CreatePalette(username, listcolors);
            return x;
        }

        [HttpGet("Partialonecolor/{username}/{color}")]
        public IActionResult Partialonecolor(string username, string color)//return partial
        {
            Palette.AddColor(username, color);
            var res = new PartialViewResult()
            {
                ViewName = "_partialonecolor",
                ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = color
                }
            };
            return res;
        }

        [HttpGet("deleteonecolor/{username}/{color}")]//delete single color
        public int deleteonecolor(string username, string color)
        {
            int x = Palette.RemoveColor(username, color);
            return x;
        }
    }
}