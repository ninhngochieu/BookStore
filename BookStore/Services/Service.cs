using System;
using System.IO;
using AutoMapper;
using BookStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BookStore.Services
{
    public abstract class Service
    {
        protected readonly bookstoreContext _bookstoreContext;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public readonly IMapper _mapper;

        public Service(bookstoreContext bookstoreContext, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _bookstoreContext = bookstoreContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        public void UploadImage(IFormFile file, string AvatarName)
        {
            if (!Directory.Exists(_webHostEnvironment.WebRootPath + "/Images"))
            {
                Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "/Images");
            }

            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", AvatarName);
            file.CopyToAsync(new FileStream(filePath, FileMode.Create));
        }

    }
}
