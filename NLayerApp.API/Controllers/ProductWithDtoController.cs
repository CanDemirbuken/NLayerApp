using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.API.Filters;
using NLayerApp.Core.DTOs;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Services;

namespace NLayerApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWithDtoController : CustomBaseController
    {
        private readonly IProductServiceWithDto _productServiceWithDto;

        public ProductWithDtoController(IProductServiceWithDto productServiceWithDto)
        {
            _productServiceWithDto = productServiceWithDto;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _productServiceWithDto.GetProductsWithCategory());
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _productServiceWithDto.GetAllAsync();
            return CreateActionResult(products);
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productServiceWithDto.GetByIdAsync(id);
            return CreateActionResult(product);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto productCreateDto)
        {
            var product = await _productServiceWithDto.AddAsync(productCreateDto);
            return CreateActionResult(product);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            var product = await _productServiceWithDto.UpdateAsync(productUpdateDto);
            return CreateActionResult(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productServiceWithDto.RemoveAsync(id);

            return CreateActionResult(product);
        }

        [HttpPost("SaveAll")]
        public async Task<IActionResult> Save(List<ProductDto> productDtos)
        {
            var products = await _productServiceWithDto.AddRangeAsync(productDtos);
            return CreateActionResult(products);
        }

        [HttpDelete("RemoveAll")]
        public async Task<IActionResult> RemoveAll(List<int> id)
        {
            var products = await _productServiceWithDto.RemoveRangeAsync(id);
            return CreateActionResult(products);
        }

        [HttpGet("Any/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            var products = await _productServiceWithDto.AnyAsync(x => x.Id == id);
            return CreateActionResult(products);
        }
    }
}
