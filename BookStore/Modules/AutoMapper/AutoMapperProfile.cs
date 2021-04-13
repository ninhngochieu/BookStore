using AutoMapper;
using BookStore.Models;
using BookStore.View_Models;
using BookStore.View_Models.Author;
using BookStore.View_Models.Book;
using BookStore.View_Models.BookComment;
using BookStore.View_Models.Category;
using BookStore.View_Models.User;
using BookStore.ViewModels.BookImage;
using BookStore.ViewModels.Cart;
using BookStore.ViewModels.CityAddress;
using BookStore.ViewModels.DistrictAddress;
using BookStore.ViewModels.Invoice;
using BookStore.ViewModels.Role;
using BookStore.ViewModels.User;
using BookStore.ViewModels.UserAddress;

namespace BookStore.Modules.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Role
            CreateMap<Role, RoleViewModel>();

            //District Address
            CreateMap<DistrictAddress, DistrictAddressViewModel>();

            //City Address
            CreateMap<CityAddress, CityAddressViewModel>();

            //Address
            CreateMap<UserAddress, UserAddressViewModel>()
                .ForMember(s=>s.CityAddress, options=> options.MapFrom(s=>s.CityAddress))
                .ForMember(s=>s.DistrictAddress, options=>options.MapFrom(s=>s.DistrictAddress));

            //User
            CreateMap<User, UserInfoViewModel>()
                .ForSourceMember(u => u.RefreshToken, options => options.DoNotValidate())
                .ForSourceMember(u => u.Username, options => options.DoNotValidate())
                .ForSourceMember(u => u.Role, options => options.DoNotValidate())
                .ForSourceMember(u => u.RoleId, options => options.DoNotValidate())
                .ForSourceMember(u => u.TokenCreateAt, options => options.DoNotValidate())
                .ForSourceMember(u => u.Id, options => options.DoNotValidate())
                .ForSourceMember(u => u.Password, options => options.DoNotValidate())
                .ForMember(s=>s.Addresses, options=>options.MapFrom(s=>s.Addresses))
                ;

            CreateMap<User, UserAdminViewModel>()
                .ForMember(s=>s.RoleViewModel, options=>options.MapFrom(s=>s.Role))
                .ForMember(s=>s.Addresses, options=>options.MapFrom(s=>s.Addresses))
                ;

            //Book
            CreateMap<BookImage, BookImageViewModel>();

            CreateMap<Book, BookInfoViewModel>()
                .ForMember(d => d.AuthorName, options => options.MapFrom(s => s.Author.AuthorName))
                .ForMember(d => d.CategoryName, options => options.MapFrom(s => s.Category.CategoryName))
                .ForMember(d=>d.BookImageViewModel,option=>option.MapFrom(s=>s.BookImage));
                            //.ForMember(d => d.Image1, options => options.MapFrom(s => s.BookImage.Image1))
                            //.ForMember(d => d.Image2, options => options.MapFrom(s => s.BookImage.Image2))
                            //.ForMember(d => d.Image3, options => options.MapFrom(s => s.BookImage.Image3))
                            //.ForMember(d => d.Image4, options => options.MapFrom(s => s.BookImage.Image4));
            CreateMap<CreateNewBookDTO, Book>()
                .ForSourceMember(s => s.MainImage, options => options.DoNotValidate())
                .ForSourceMember(s => s.Image4, options => options.DoNotValidate())
                .ForSourceMember(s => s.Image1, options => options.DoNotValidate())
                .ForSourceMember(s => s.Image2, options => options.DoNotValidate())
                .ForSourceMember(s => s.Image3, options => options.DoNotValidate());

            //Category
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, DeletedCategoryViewModel>();
            CreateMap<CreateNewCategoryDTO, Category>();

            //Author
            CreateMap<Author, AuthorPostModel>()
                .ForSourceMember(s => s.Id, options => options.DoNotValidate());
            CreateMap<AuthorPostModel, Author>();

            //Comment
            CreateMap<BookCommentDTO, BookComment>();
            CreateMap<BookComment, BookCommentViewModel>()
                .ForSourceMember(s => s.Book, options => options.DoNotValidate())
                .ForSourceMember(s => s.User, options => options.DoNotValidate());

            //Invoice
            CreateMap<InvoicePostModel, Invoice>();

            //Cart
            CreateMap<Cart, InvoiceDetail>()
                .ForSourceMember(s => s.UserId, options => options.DoNotValidate())
                .ForSourceMember(s => s.User, options => options.DoNotValidate())
                .ForSourceMember(s => s.Id, options => options.DoNotValidate())
                .ForMember(d => d.Quantity, options => options.MapFrom(s => s.Amount));
            CreateMap<Cart, CartViewModel>();

            //Address
            CreateMap<UserAddressPostModel, UserAddress>();

        }

    }
}
