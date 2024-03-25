using AutoMapper;
using NLayerApp.Core.DTOs;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services;
using NLayerApp.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Service.Services
{
    public class CategoryMVCService : Service<Category>, ICategoryMVCService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryMVCService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryWithProductDto> GetCategoryByIdWithProducts(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdWithProducts(id);
            var categoryDto = _mapper.Map<CategoryWithProductDto>(category);

            return categoryDto;
        }
    }
}
