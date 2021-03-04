using System;
using AutoMapper;
using BookStore.Models;
using Microsoft.AspNetCore.Hosting;

namespace BookStore.Services
{
    public class ImageServices : Service
    {
        public ImageServices(bookstoreContext bookstoreContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(bookstoreContext, webHostEnvironment, mapper)
        {
        }
    }
}
