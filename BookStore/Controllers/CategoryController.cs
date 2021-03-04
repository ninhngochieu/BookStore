using BookStore.Models;
using BookStore.Services;
using BookStore.View_Models.Category;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IList<CategoryViewModel>> GetAll() => await _categoryService.GetAllCategories();

        [HttpGet]
        [Route("GetCategoryById")]
        public async Task<CategoryViewModel> GetCategoryById(int id) => await _categoryService.GetCategoryById(id);

        [HttpGet]
        [Route("GetCategoryBooksById")]
        public async Task<Category> GetCategoryBooksById(int id) => await _categoryService.GetCategoryBooksById(id);

        [HttpPost]
        [Route("CreateNewCategory")]
        public async Task<CategoryViewModel> CreateNewCategory([FromBody] CreateNewCategoryDTO model) => await _categoryService.CreateNewCategory(model);

        
        [HttpPut]
        [Route("ChangeCategoryName")]
        public async Task<IActionResult> Put([FromBody] ChangeCategoryNameDTO model)
        {
            var changedCategory = await _categoryService.ChangeCategoryName(model);
            if(changedCategory.Id == 0)
            {
                return NotFound();
            }
            return Ok(changedCategory) ;
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete]
        [Route("DeleteCategory")]
        public async Task<IActionResult> Delete(int id)
        { 
            var deletedCategory = await _categoryService.DeleteCategory(id); 
            if(deletedCategory.DeletedStatus)
            {
                return Ok(deletedCategory);
            }
            else
            {
                return NotFound();
            }
        }
        
    }
}
