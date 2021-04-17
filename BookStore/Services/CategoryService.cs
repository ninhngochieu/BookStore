using AutoMapper;
using BookStore.Models;
using BookStore.View_Models.Category;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class CategoryService : Service
    {
        private readonly BookServices _bookServices;

        public CategoryService(BookServices bookServices,bookstoreContext bookstoreContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(bookstoreContext, webHostEnvironment, mapper)
        {
            _bookServices = bookServices;
        }

        public async Task<IList<CategoryViewModel>> GetAllCategories()
        {
            return _mapper.Map<IList<CategoryViewModel>>(await _bookstoreContext.Categories.ToListAsync());
        }

        public async Task<CategoryViewModel> GetCategoryById(int id)
        {
            return _mapper.Map<CategoryViewModel>(await _bookstoreContext.Categories.FindAsync(id));
        }

        public async Task<CategoryViewModel> CreateNewCategory(CreateNewCategoryDTO model)
        {
            bool existingCategory = _bookstoreContext.Categories.Any(c => c.CategoryName == model.CategoryName);
            if (existingCategory)
            {
                throw new InvalidOperationException("Category already exist!");
            }
            else
            {
                var newCategory = _mapper.Map<Category>(model);
                await _bookstoreContext.Categories.AddAsync(newCategory);
                await _bookstoreContext.SaveChangesAsync();
                return _mapper.Map<CategoryViewModel>(newCategory);
            }
        }

        public async Task<Category> GetCategoryBooksById(int id)
        {
            return await _bookstoreContext.Categories.FindAsync(id);
        }

        public async Task<IList<Category>> GetAllCategoryBooks()
        {
            return await _bookstoreContext.Categories.ToListAsync();
        }

        public async Task<CategoryViewModel> ChangeCategoryName(ChangeCategoryNameDTO model)
        {
            bool existingCategory = _bookstoreContext.Categories.Any(c => c.Id == model.Id);

            if(!existingCategory)
            {
                return new CategoryViewModel();
            }
            else
            {
                var category = await _bookstoreContext.Categories.FindAsync(model.Id);
                category.CategoryName = model.CategoryName;
                _bookstoreContext.Entry(category).State = EntityState.Modified;
                await _bookstoreContext.SaveChangesAsync();
                return _mapper.Map<CategoryViewModel>(category);
            }
        }

        public async Task<DeletedCategoryViewModel> DeleteCategory(int id)
        {
            var existingCategory = await _bookstoreContext.Categories.FindAsync(id);
            var model = _mapper.Map<DeletedCategoryViewModel>(existingCategory);
            if(model == null)
            {
                model = new DeletedCategoryViewModel();
                model.DeletedStatus = false;
            }
            else
            {
                model.DeletedStatus = true;
                _bookstoreContext.Entry(existingCategory).State = EntityState.Deleted;
                await _bookstoreContext.SaveChangesAsync();
            }
            return model;
        }
    }
}
