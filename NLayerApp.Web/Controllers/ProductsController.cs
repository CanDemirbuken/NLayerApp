using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayerApp.Core.DTOs;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Services;

namespace NLayerApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductMVCService _productMVCService;
        private readonly ICategoryMVCService _categoryMVCService;
        private readonly IMapper _mapper;

        public ProductsController(IProductMVCService productMVCService, ICategoryMVCService categoryMVCService, IMapper mapper)
        {
            _productMVCService = productMVCService;
            _categoryMVCService = categoryMVCService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productMVCService.GetProductsWithCategory();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Save()
        {
            var categories = await _categoryMVCService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(productDto);
                await _productMVCService.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryMVCService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            return View();
        }


        // Constructor methodunda parametre alan filter'lar ServiceFilter kullanılarak yazılır.
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productMVCService.GetByIdAsync(id);
            var categories = await _categoryMVCService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);

            var productDto = _mapper.Map<ProductDto>(product);
            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(productDto);
                await _productMVCService.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryMVCService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }

        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productMVCService.GetByIdAsync(id);
            await _productMVCService.RemoveAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
