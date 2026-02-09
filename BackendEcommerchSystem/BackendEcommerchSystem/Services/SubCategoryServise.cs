using BackendEcommerchSystem.DTOs.SubCategoryDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;
using BackendEcommerchSystem.Repositorie;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging.Abstractions;

namespace BackendEcommerchSystem.Services
{
    public class SubCategoryServise : ISubCategoryService
    {
      private readonly ISubCategoryRepository _subCategoryRepository;

        public SubCategoryServise(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository; 
        }
        public async Task<IEnumerable<SubCategoryDTO>> GetAllAsync()
        {
            var subCategories = await _subCategoryRepository.GetAllAsync();

            return subCategories.Select(x => new SubCategoryDTO
            {
                SubCategoryId = x.SubCategoryId,
                Name = x.Name,
                CategoryId = x.CategoryId,
                CategoryName = x.Category != null ? x.Category.Name : null
            }).ToList();
        }

        public async Task<SubCategoryDTO> GetByIdAsync(int id)
        {
            var subCategory = await _subCategoryRepository.GetSubCategoryById(id);
            if (subCategory == null)
                return null;

            return new SubCategoryDTO
            {
                SubCategoryId = subCategory.SubCategoryId,
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId,
                CategoryName = subCategory.Category != null ? subCategory.Category.Name : null
            };
        }

        public async Task<SubCategoryDTO> CreateAsync(SubCreateDTO dto)
        {
            var subCategory = new SubCategory
            {
                Name = dto.Name,
                CategoryId = dto.CategoryId
            };

            await _subCategoryRepository.AddAsync(subCategory);
            await _subCategoryRepository.SaveChangeAsync();

            return new SubCategoryDTO
            {
                SubCategoryId = subCategory.SubCategoryId,
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId
            };
        }



        public async Task<SubCategoryDTO> UpdateAsync(int id, UpdateSubCategoryDTO dto)
        {
            var subCategory = await _subCategoryRepository.GetSubCategoryById(id);
            if (subCategory == null)
                return null;

            subCategory.Name = dto.Name;
            subCategory.CategoryId = dto.CategoryId;

            await _subCategoryRepository.UpdateAsync(subCategory);
            await _subCategoryRepository.SaveChangeAsync();

            return new SubCategoryDTO
            {
                SubCategoryId = subCategory.SubCategoryId,
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId,
                CategoryName = subCategory.Category != null ? subCategory.Category.Name : null
            };
        }

        public async Task<SubCategoryDTO> DeleteAsync(int id)
        {
            var subCategory = await _subCategoryRepository.GetSubCategoryById(id);
            if (subCategory == null)
                return null;

            await _subCategoryRepository.DeleteAsync(id);
            await _subCategoryRepository.SaveChangeAsync();

            return new SubCategoryDTO
            {
                SubCategoryId = subCategory.SubCategoryId,
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId,
                CategoryName = subCategory.Category != null ? subCategory.Category.Name : null
            };
        }

    }




}
