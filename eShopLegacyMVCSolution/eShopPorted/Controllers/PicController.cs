using eShopPorted.Services;
using log4net;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace eShopPorted.Controllers
{
    public class PicController : ControllerBase
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const string GetPicRouteName = "GetPicRouteTemplate";

        private readonly ICatalogService service;

        public PicController(ICatalogService service)
        {
            this.service = service;
        }

        // GET: Pic/5.png
        [HttpGet("items/{catalogItemId:int}/pic", Name = GetPicRouteName)]
        public IActionResult Index(int catalogItemId)
        {
            _log.Info($"Now loading... /items/Index?{catalogItemId}/pic");

            if (catalogItemId <= 0)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }

            var item = service.FindCatalogItem(catalogItemId);

            if (item != null)
            {
                var webRoot = "";
                var path = Path.Combine(webRoot, item.PictureFileName);

                string imageFileExtension = Path.GetExtension(item.PictureFileName);
                string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

                var buffer = System.IO.File.ReadAllBytes(path);

                return new FileContentResult(buffer, mimetype);
            }

            return NotFound();
        }

        private static string GetImageMimeTypeFromImageFileExtension(string extension)
        {
            return extension switch
            {
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".bmp" => "image/bmp",
                ".tiff" => "image/tiff",
                ".wmf" => "image/wmf",
                ".jp2" => "image/jp2",
                ".svg" => "image/svg+xml",
                _ => MediaTypeNames.Application.Octet,
            };
        }
    }
}