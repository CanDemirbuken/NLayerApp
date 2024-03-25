using NLayerApp.Core.DTOs;
using NLayerApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Services
{
    public interface ICategoryMVCService : IService<Category>
    {
        Task<CategoryWithProductDto> GetCategoryByIdWithProducts(int id);
    }
}
