using AutoMapper;
using BookStore.Models;
using BookStore.View_Models;
using BookStore.View_Models.Author;
using BookStore.View_Models.Book;
using BookStore.View_Models.BookComment;
using BookStore.View_Models.Category;
using BookStore.View_Models.User;

namespace BookStore.Modules.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Role, RoleViewModel>()
                .ForSourceMember(u => u.Users, options => options.DoNotValidate());
            CreateMap<RoleViewModel, Role>();

            CreateMap<User, UserInfoViewModel>()
                .ForSourceMember(u => u.RefreshToken, options => options.DoNotValidate())
                .ForSourceMember(u => u.Username, options => options.DoNotValidate())
                .ForSourceMember(u => u.Role, options => options.DoNotValidate())
                .ForSourceMember(u => u.RoleId, options => options.DoNotValidate())
                .ForSourceMember(u => u.TokenCreateAt, options => options.DoNotValidate())
                .ForSourceMember(u => u.Id, options => options.DoNotValidate())
                .ForSourceMember(u => u.Password, options => options.DoNotValidate());

            CreateMap<Book, BookInfoViewModel>()
                .ForMember(d => d.AuthorName, options => options.MapFrom(s => s.Author.AuthorName))
                .ForMember(d => d.CategoryName, options => options.MapFrom(s => s.Category.CategoryName))
                            .ForMember(d => d.Image1, options => options.MapFrom(s => s.BookImage.Image1))
                            .ForMember(d => d.Image2, options => options.MapFrom(s => s.BookImage.Image2))
                            .ForMember(d => d.Image3, options => options.MapFrom(s => s.BookImage.Image3))
                            .ForMember(d => d.Image4, options => options.MapFrom(s => s.BookImage.Image4));
            CreateMap<CreateNewBookDTO, Book>()
                .ForSourceMember(s => s.MainImage, options => options.DoNotValidate())
                .ForSourceMember(s => s.Image4, options => options.DoNotValidate())
                .ForSourceMember(s => s.Image1, options => options.DoNotValidate())
                .ForSourceMember(s => s.Image2, options => options.DoNotValidate())
                .ForSourceMember(s => s.Image3, options => options.DoNotValidate());

            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, DeletedCategoryViewModel>();
            CreateMap<CreateNewCategoryDTO, Category>();
            CreateMap<Author, AuthorPostModel>()
                .ForSourceMember(s => s.Id, options => options.DoNotValidate());
            CreateMap<AuthorPostModel, Author>();

            CreateMap<BookCommentDTO, BookComment>();
            CreateMap<BookComment, BookCommentViewModel>()
                .ForSourceMember(s => s.Book, options => options.DoNotValidate())
                .ForSourceMember(s => s.User, options => options.DoNotValidate());
        }
        
    }
}
