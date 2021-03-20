﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Models;
using BookStore.View_Models.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;

namespace BookStore.Services
{
    public class UserServices : Service
    {
        public UserServices(bookstoreContext bookstoreContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(bookstoreContext, webHostEnvironment, mapper)
        {
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
            return await _bookstoreContext.Users
                .Where(u=>u.RefreshToken.Equals(refresh))
                .Include(r=>r.Role)
                .FirstOrDefaultAsync();
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
            if (userVM.Phone is not null)
            {
                user.Phone = userVM.Phone;
            }

            if (userVM.Avatar is not null)
            {
                user.Avatar = DateTimeOffset.Now.ToUnixTimeSeconds()+"_"+userVM.Avatar.FileName;
            }
            _bookstoreContext.Entry(user).State = EntityState.Modified;
            try
            {
                bool isUpdateUserSuccess = await _bookstoreContext.SaveChangesAsync() != 0;
                if (isUpdateUserSuccess&&userVM is not null)
                {
                    if(userVM.Avatar is not null)
                    {
                        UploadImage(userVM.Avatar, user.Avatar);
                    }
                    userInfoViewModel = _mapper.Map<UserInfoViewModel>(user);
                }

                UserAddress userAddress = await _bookstoreContext.UserAddress
                    .Where(u => u.UserId == user.Id)
                    .Where(u=>u.IsDefault==true)
                    .FirstOrDefaultAsync();
                if(userAddress is not null)
                {
                    //Update
                    if(userVM.CityAddressId is not null)
                    {
                        userAddress.CityAddressId = userVM.CityAddressId??0;
                    }
                    if(userVM.DistrictAddressId is not null)
                    {
                        userAddress.DistrictAddressId = userVM.DistrictAddressId??0;
                    }
                    if (userVM.Name is not null)
                    {
                        userAddress.Name = userVM.Name;
                    }
                    if (userVM.Phone is not null)
                    {
                        userAddress.Phone = userVM.Phone;
                    }
                    _bookstoreContext.Entry(userAddress).State = EntityState.Modified;

                }
                else
                {
                    //Create
                    UserAddress newUserAddress = new UserAddress
                    {
                        UserId = user.Id,
                        IsDefault = true
                    };
                    if (userVM.CityAddressId is not null)
                    {
                        newUserAddress.CityAddressId = userVM.CityAddressId ?? 0;
                    }
                    if (userVM.DistrictAddressId is not null)
                    {
                        newUserAddress.DistrictAddressId = userVM.DistrictAddressId??0;
                    }
                    if(userVM.Name is not null)
                    {
                        newUserAddress.Name= userVM.Name;
                    }
                    if (userVM.Phone is not null)
                    {
                        newUserAddress.Phone = userVM.Phone;
                    }
                    _bookstoreContext.Add(newUserAddress);
                }
                isUpdateUserSuccess = await _bookstoreContext.SaveChangesAsync() != 0;

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return userInfoViewModel;

        }

        internal async Task<User> GetUserById(int id)
        {
            //var address =  _bookstoreContext.UserAddress
            //    .Include(c => c.CityAddress)
            //    .Include(d => d.DistrictAddress)
            //    .Where(p => p.IsDefault == true)
            //    .Where(u=>u.UserId==id)
            //    .FirstOrDefaultAsync;
            return await _bookstoreContext.Users
                .Where(u=>u.Id==id)
                .Include(a=>a.Addresses)
                    .ThenInclude(c=>c.CityAddress)
                    .ThenInclude(d=>d.DistrictAddresses)
                .FirstOrDefaultAsync();
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

        internal async Task<bool> AddNewUser(User user)
        {
            _bookstoreContext.Users.Add(user);
            try
            {
                return await _bookstoreContext.SaveChangesAsync() != 0;
            }catch(Exception)
            {
                return false;
                throw;
            }
        }

        internal async Task<bool> ChangePasswordAsync(User user, string new_password)
        {
            user.Password = new_password;
            _bookstoreContext.Entry(user).State = EntityState.Modified;
            try
            {
                return await _bookstoreContext.SaveChangesAsync() != 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
                throw;
            }
        }

        internal async Task<User> GetUserByIdAndPasswordAsync(int id, string old_password)
        {
            return await _bookstoreContext.Users
                .Where(u => u.Id == id)
                .Where(u => u.Password.Equals(old_password)).FirstOrDefaultAsync();
        }
    }
}
