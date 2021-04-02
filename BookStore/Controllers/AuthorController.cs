using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using AutoMapper;
using BookStore.View_Models.Author;
using BookStore.Services;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly bookstoreContext _context;
        private readonly IMapper _mapper;
        private readonly AuthorServices _authorServices;

        public AuthorController(bookstoreContext context, IMapper mapper, AuthorServices authorServices)
        {
            _context = context;
            _mapper = mapper;
            _authorServices = authorServices;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthor()
        {
            return Ok(new { data = await _authorServices.GetAllAuthor(), success = true});
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            return Ok(new { data = await _authorServices.GetAuthorById(id), success = true });
        }

        // PUT: api/Author/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutAuthor(AuthorPostModel authorPostModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error_message = "Loi cu phap"});
            }
            Author author = await _authorServices.FindAuthorAsync(authorPostModel.Id);
            if (author is null)
            {
                return Ok(new { error_message= "Khong tim thay tac gia"});
            }
            bool IsUpdateAuthor = await _authorServices.UpdateAuthorAsync(author, authorPostModel);
            if (IsUpdateAuthor)
            {
                return Ok(new { data = author, success = true });
            }
            else
            {
                return Ok(new { error_message = "Khong co gi thay doi" });
            }
          
        }

        // POST: api/Author
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorPostModel authorPost)
        {
            Author author = _mapper.Map<Author>(authorPost);
            _context.Author.Add(author);
            bool IsSaveAuthor = await _authorServices.AddNewBook(author);
            if (IsSaveAuthor)
            {
                return Ok(new { data = author, success = true });
            }
            else
            {
                return Ok(new { error_message = "Co loi khi luu tac gia"});
            }
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _authorServices.FindAuthorAsync(id);
            if (author is null)
            {
                return Ok(new { error_message = "Khong tim thay tac gia" });
            }
            bool IsDeleteAuthor = await _authorServices.DeleteAuthorAsync(author);
            if (IsDeleteAuthor)
            {
                return Ok(new { data = "Xoa tac gia thanh cong", success = true });
            }
            else
            {
                return Ok(new { error_message = "Xoa that bai, co loi xay ra" });
            }
        }

    }
}
