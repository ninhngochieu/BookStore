using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Models;
using BookStore.ViewModels.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;

namespace BookStore.Services
{
    public class UserServices : Service
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserServices(bookstoreContext bookstoreContext, IMapper mapper, IWebHostEnvironment webHostEnvironment) : base(bookstoreContext)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        internal async Task<User> DoLogin(LoginViewModel user)
        {
            return await _bookstoreContext.Users
                .Where(u=>u.Username.Equals(user.Username))
                .Where(u=>u.Password.Equals(user.Password))
                .Include(r=>r.Role)
                .FirstOrDefaultAsync();
        }

        internal async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            return await _bookstoreContext.Users.Include(r => r.Role).ToListAsync();
        }

        internal async Task createUserTokenAsync(User user, string refresh)
        {
            user.RefreshToken = refresh;
            user.TokenCreateAt = DateTime.Now;
            _bookstoreContext.Entry(user).State = EntityState.Modified;
            await _bookstoreContext.SaveChangesAsync();
        }

        internal async Task<User> GetByRefreshToken(string refresh)
        {
            return await _bookstoreContext.Users.Where(u=>u.RefreshToken.Equals(refresh)).FirstOrDefaultAsync();
        }

        internal async Task<UserInfoViewModel> UpdateInfoAsync(UserInfoPostModel userVM, int id)
        {
            UserInfoViewModel userInfoViewModel = null;
            User user = await _bookstoreContext.Users.FindAsync(id);
            if (user is null) return userInfoViewModel;

            if (userVM.Name is not null)
            {
                user.Name = userVM.Name;
            }
            if(userVM.Email is not null)
            {
                user.Email = userVM.Email;
            }
            if(userVM.Avatar is not null)
            {
                user.Avatar = DateTimeOffset.Now.ToUnixTimeSeconds()+"_"+userVM.Avatar.FileName;
            }
            _bookstoreContext.Entry(user).State = EntityState.Modified;
            try
            {
                bool isUpdateSuccess = await _bookstoreContext.SaveChangesAsync() != 0;
                if (isUpdateSuccess&&userVM is not null)
                {
                    userInfoViewModel = _mapper.Map<UserInfoViewModel>(user);
                    UploadImage(userVM.Avatar, user.Avatar);
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return userInfoViewModel;

        }

        private void UploadImage(IFormFile file, string AvatarName)
        {
            if (!Directory.Exists(_webHostEnvironment.WebRootPath + "/Images"))
            {
                Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "/Images");
            }

            string filePath = Path.Combine(_webHostEnvironment.WebRootPath,"Images",AvatarName);
            file.CopyToAsync(new FileStream(filePath,FileMode.Create));
        }

        internal async Task<User> GetUserById(int id)
        {
            return await _bookstoreContext.Users.FindAsync(id);
        }

        internal bool isValidImage(IFormFile postedFile)
        {
            int ImageMinimumBytes = 2048;
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                    postedFile.ContentType.ToLower() != "image/jpeg" &&
                    postedFile.ContentType.ToLower() != "image/pjpeg" &&
                    postedFile.ContentType.ToLower() != "image/gif" &&
                    postedFile.ContentType.ToLower() != "image/x-png" &&
                    postedFile.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.OpenReadStream().CanRead)
                {
                    return false;
                }
                //------------------------------------------
                //check whether the image size exceeding the limit or not
                //------------------------------------------ 
                if (postedFile.Length < ImageMinimumBytes)
                {
                    return false;
                }

                byte[] buffer = new byte[ImageMinimumBytes];
                postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.OpenReadStream()))
                {
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                postedFile.OpenReadStream().Position = 0;
            }

            return true;
        }
    }
}
