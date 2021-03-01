using AutoMapper;
using BookStore.Models;
using BookStore.View_Models.BookDetailViewModels;
using BookStore.View_Models.BookViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly bookstoreContext _context;
        private readonly DbSet<Book> _bookService;
        private readonly DbSet<BookDetail> _bookDetailsService;
        private readonly IMapper _mapper;

        public BookController(bookstoreContext context, IMapper mapper)
        {
            _context = context;
            _bookService = _context.Set<Book>();
            _bookDetailsService = _context.Set<BookDetail>();
            _mapper = mapper;
        }


        // GET: api/<BookController>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IList<GetAllBooksViewModel>> GetAll()
        {
            var bookVm = _mapper.Map<IList<Book>, IList<GetAllBooksViewModel>>(await _bookService.ToListAsync<Book>());
            return bookVm;
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Tìm sách và chi tiết sách theo Id sách
            var book = await _bookService.SingleOrDefaultAsync(b => b.Id == id);
            var bookDetail = await _bookDetailsService.SingleOrDefaultAsync(b => b.BookId == id);

            if (book == null || bookDetail == null)
            {
                return NotFound();
            }

            // Map sách và chi tiết sách vào cùng 1 view model
            var result = MapBookAndDetailGet(book, bookDetail);

            return Ok(result);
        }

        private GetBookByIdViewModel MapBookAndDetailGet(Book book, BookDetail bookDetail)
        {
            return new GetBookByIdViewModel
            {
                //Id = book.Id,
                //BookName = book.BookName,
                //Price = book.Price,
                //Rating = book.Rating,
                //CoverType = bookDetail.CoverType,
                //Description = bookDetail.Description,
                //DistributorId = bookDetail.DistributorId,
                //Pages = bookDetail.Pages,
                //PublicationDate = bookDetail.PublicationDate,
                //PublisherId = bookDetail.PublisherId,
                //Quantity = bookDetail.Quantity,
                //Size = bookDetail.Size,
                //SKU = bookDetail.SKU,
            };
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddNewBookViewModel bookVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Sau khi SaveChanges thì book sẽ tự sinh Id
            var book = _mapper.Map<Book>(bookVm);
            await _bookService.AddAsync(book);
            await _context.SaveChangesAsync();
            
            // Cái này dùng AutoMapper lỗi lên xuống nên tạm thời map tay bằng MapBookAndDetail()
            // Truyền tay Id sách ở trên xuống dưới
            //var bookDetailVm = MapBookAndDetailAdd(book.Id, bookVm);

            //// Thêm chi tiết sách
            //_bookDetailsService.Add(_mapper.Map<BookDetail>(bookDetailVm));
            //await _context.SaveChangesAsync();

            // Trả về thông tin sách được thêm
            return Created("Book created:", book);
        }

        private AddNewBookDetailViewModel MapBookAndDetailAdd(int id, AddNewBookViewModel model)
        {
            return new AddNewBookDetailViewModel
            {
                BookId = id,
                Quantity = model.Quantity,
                DistributorId = model.DistributorId,
                PublicationDate = model.PublicationDate,
                CoverType = model.CoverType,
                Description = model.Description,
                Pages = model.Pages,
                PublisherId = model.PublisherId,
                Size = model.Size,
                SKU = model.SKU,
            };
        }

        // PUT api/<BookController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditBookViewModel model)
        {
            var book = await _bookService.SingleOrDefaultAsync(b => b.Id == model.Id);
            var bookDetail = await _bookDetailsService.SingleOrDefaultAsync(bd => bd.BookId == model.Id);

            if (book != null && bookDetail != null)
            {
                MapBookAndDetailEdit(book, bookDetail, model);
            }
            else
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return Ok(book);
        }

        private void MapBookAndDetailEdit(Book book, BookDetail bookDetail, EditBookViewModel model)
        {
            //book.BookName = model.BookName;
            //book.Price = model.Price;
            //book.Rating = model.Rating;
            //bookDetail.Quantity = model.Quantity;
            //bookDetail.DistributorId = model.DistributorId;
            //bookDetail.PublicationDate = model.PublicationDate;
            //bookDetail.Size = model.Size;
            //bookDetail.CoverType = model.CoverType;
            //bookDetail.Pages = model.Pages;
            //bookDetail.SKU = model.SKU;
            //bookDetail.PublisherId = model.PublisherId;
            //bookDetail.Description = model.Description;
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.SingleOrDefaultAsync(b => b.Id == id);
            var bookDetail = await _bookDetailsService.SingleOrDefaultAsync(bd => bd.BookId == id);

            if (book != null && bookDetail != null)
            {
                _context.Entry(book).State = EntityState.Deleted;
                _context.Entry(bookDetail).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }
    }
}
