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

        // GET: api/<BookCommentsController>
        [HttpGet]
        [Route("GetCommentsInBook")]
        public async Task<IActionResult> GetCommentsInBook(int bookId) => Ok(await _bookCommentServices.GetCommentsInBook(bookId));

        // GET api/<BookCommentsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookCommentsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BookCommentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookCommentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
