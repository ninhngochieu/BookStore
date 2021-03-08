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
        public async Task<IActionResult> GetAll()
        {
            return Ok(new { data = await _categoryService.GetAllCategories(), success = true});
        }

        [HttpGet]
        [Route("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            return Ok(new { data = await _categoryService.GetCategoryById(id), success = true });

        }

        [HttpGet]
        [Route("GetCategoryBooksById")]
        public async Task<IActionResult> GetCategoryBooksById(int id)
        {
            return Ok(new { data =  await _categoryService.GetCategoryBooksById(id), success = true });

        }

        [HttpPost]
        [Route("CreateNewCategory")]
        public async Task<IActionResult> CreateNewCategory([FromBody] CreateNewCategoryDTO model)
        {
            return Ok(new { data = await _categoryService.CreateNewCategory(model), success = true });
        }
        
        [HttpPut]
        [Route("ChangeCategoryName")]
        public async Task<IActionResult> Put([FromBody] ChangeCategoryNameDTO model)
        {
            var changedCategory = await _categoryService.ChangeCategoryName(model);
            if(changedCategory.Id == 0)
            {
                return NotFound();
            }
            return Ok(new { data = changedCategory, success = true}) ;
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete]
        [Route("DeleteCategory")]
        public async Task<IActionResult> Delete(int id)
        { 
            var deletedCategory = await _categoryService.DeleteCategory(id); 
            if(deletedCategory.DeletedStatus)
            {
                return Ok(new { data = deletedCategory, success = true});
            }
            else
            {
                return NotFound();
            }
        }
        
    }
}
