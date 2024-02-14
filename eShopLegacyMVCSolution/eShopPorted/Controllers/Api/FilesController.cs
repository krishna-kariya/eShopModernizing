using eShopLegacy.Utilities;
using eShopPorted.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace eShopPorted.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly ICatalogService _service;

        public FilesController(ICatalogService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public IActionResult Index()
        {
            var brands = _service.GetCatalogBrands()
                .Select(b => new BrandDTO(b.Id, b.Brand)).ToList();

            var data = Serializing.SerializeBinary(brands);

            return Ok(data);
        }

        [Serializable]
        public class BrandDTO
        {
            public int Id { get; set; }
            public string Brand { get; set; }

            public BrandDTO(int id, string brand)
            {
                Id = id;
                Brand = brand;
            }
        }
    }
}