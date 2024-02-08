using eShopLegacy.Utilities;
using eShopLegacyMVC.Services;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace eShopLegacyMVC.Controllers.WebApi
{
    public class FilesController : ControllerBase
    {
        private ICatalogService _service;

        public FilesController(ICatalogService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET api/<controller>
        public IActionResult Get()
        {
            var brands = _service.GetCatalogBrands()
                .Select(b => new BrandDTO
                {
                    Id = b.Id,
                    Brand = b.Brand
                }).ToList();

            return Ok(brands);
        }

        [Serializable]
        public class BrandDTO
        {
            public int Id { get; set; }
            public string Brand { get; set; }
        }
    }
}