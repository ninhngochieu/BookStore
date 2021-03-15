using BookStore.Services;
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
    public class BookCommentsController : ControllerBase
    {
        private readonly BookCommentServices _bookCommentServices;

        public BookCommentsController(BookCommentServices bookCommentServices)
        {
            _bookCommentServices = bookCommentServices;
        }

        [HttpGet]
        [Route("GetCommentsInBook")]
        public async Task<IActionResult> GetCommentsInBook(int bookId) => Ok(await _bookCommentServices.GetCommentsInBook(bookId));

        [HttpPost]
        [Route("AddNewComment")]
        public async Task<IActionResult> AddNewComment([FromBody] View_Models.BookComment.BookCommentDTO model) => Ok(await _bookCommentServices.AddNewComment(model));



        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
