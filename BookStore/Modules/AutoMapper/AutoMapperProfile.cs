using AutoMapper;
using BookStore.Models;
using BookStore.View_Models.BookDetailViewModels;
using BookStore.View_Models.BookViewModels;
using System.Collections.Generic;

namespace BookStore.Modules.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // ViewModels to Objects
            CreateMap<AddNewBookViewModel, Book>()
                .ForMember(destination => destination.BookName, options => options.MapFrom(source => source.BookName))
                .ForMember(destination => destination.Price, options => options.MapFrom(source => source.Price));

            CreateMap<AddNewBookDetailViewModel, BookDetail>();

            // Objects to ViewModels
            CreateMap<Book, GetAllBooksViewModel>();


            // ViewModels to ViewModels

        }
    }
}
