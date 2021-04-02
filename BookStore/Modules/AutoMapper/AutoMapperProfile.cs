using AutoMapper;
using BookStore.Models;
using BookStore.View_Models;
using BookStore.View_Models.Author;
using BookStore.View_Models.Book;
using BookStore.View_Models.BookComment;
using BookStore.View_Models.Category;
using BookStore.View_Models.User;
using BookStore.ViewModels.Cart;
using BookStore.ViewModels.Invoice;
using BookStore.ViewModels.UserAddress;

namespace BookStore.Modules.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            
            //user
            CreateMap<User, UserInfoViewModel>()
                .ForSourceMember(u => u.RefreshToken, options => options.DoNotValidate())
                .ForSourceMember(u => u.Username, options => options.DoNotValidate())
                .ForSourceMember(u => u.Role, options => options.DoNotValidate())
                .ForSourceMember(u => u.RoleId, options => options.DoNotValidate())
                .ForSourceMember(u => u.TokenCreateAt, options => options.DoNotValidate())
                .ForSourceMember(u => u.Id, options => options.DoNotValidate())
                .ForSourceMember(u => u.Password, options => options.DoNotValidate());
                //.ForMember(c=>c.CityAddressId, options => options.MapFrom(a=>a.Addresses.FirstOrDefault().CityAddressId))
                //.ForMember(c => c.StreetAddress, options => options.MapFrom(a => a.Addresses.FirstOrDefault().Street_Address))
                //.ForMember(c => c.DistrictAddressId, options => options.MapFrom(a => a.Addresses.FirstOrDefault().DistrictAddressId))
                //.ForMember(c => c.DistrictName, options => options.MapFrom(a => a.Addresses.FirstOrDefault().DistrictAddress.DistrictName))
                //.ForMember(c => c.CityName, options => options.MapFrom(a => a.Addresses.FirstOrDefault().CityAddress.CityName))
                //.ForSourceMember(u => u.Addresses., options => options.DoNotValidate())
                //.ForSourceMember(u => u.Addresses.FirstOrDefault().DistrictAddress, options => options.DoNotValidate())
                //.ForSourceMember(u => u.Addresses.FirstOrDefault().User, options => options.DoNotValidate());

            //book
            CreateMap<Book, BookInfoViewModel>()
                .ForMember(d => d.AuthorName, options => options.MapFrom(s => s.Author.AuthorName))
                .ForMember(d => d.CategoryName, options => options.MapFrom(s => s.Category.CategoryName))
                            .ForMember(d => d.Image1, options => options.MapFrom(s => s.BookImage.Image1))
                            .ForMember(d => d.Image2, options => options.MapFrom(s => s.BookImage.Image2))
                            .ForMember(d => d.Image3, options => options.MapFrom(s => s.BookImage.Image3))
                            .ForMember(d => d.Image4, options => options.MapFrom(s => s.BookImage.Image4));
            //User
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
