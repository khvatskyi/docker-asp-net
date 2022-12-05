using AutoMapper;
using MusicStreamServiceApp.BLL.DTOs;
using MusicStreamServiceApp.BLL.Interfaces.IServices;
using MusicStreamServiceApp.DAL.Entities;
using MusicStreamServiceApp.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;

namespace MusicStreamServiceApp.BLL.Services
{
    public class AccountService : SetOfFields, IAccountService
    {
        public AccountService(IUnitOfWork unitOfWork,
             IMapper mapper)
             : base(unitOfWork, mapper)
        {
        }

        public async Task<IdentityResult> CreateUserAsync(UserDTO userDTO)
        {
            var user = mapper.Map<User>(userDTO);

            user.NormalizedEmail = unitOfWork.UserManager.NormalizeEmail(userDTO.Email);

            user.NormalizedUserName = unitOfWork.UserManager.NormalizeName(userDTO.UserName);

            user.EmailConfirmed = true;

            var result = await unitOfWork.UserManager.CreateAsync(user, userDTO.Password);

            if (result.Succeeded)
            {
                await unitOfWork.SignInManager.SignInAsync(user, false);
            }

            return result;
        }

        public async Task<SignInResult> AuthenticateUserAsync(UserLoginDTO userDTO)
        {
            var user = await unitOfWork.UserManager.FindByEmailAsync(userDTO.Email);
            try
            {
                var result = await unitOfWork.SignInManager.PasswordSignInAsync(user, userDTO.Password, userDTO.RememberMe, false);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task SignOutUserAsync()
        {
            await unitOfWork.SignInManager.SignOutAsync();
        }

        public async Task<UserDTO> GetUserByEmailAsync(string Email)
        {
            var user = await unitOfWork.UserManager.FindByEmailAsync(Email);

            return mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO> GetUserByIdAsync(string Id)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(Id);

            return mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO> GetUserByUsernameAsync(string UserName)
        {
            var user = await unitOfWork.UserManager.FindByNameAsync(UserName);

            return mapper.Map<UserDTO>(user);
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var userList = await unitOfWork.UserManager.Users.ToListAsync();

            return mapper.Map<IEnumerable<UserDTO>>(userList);
        }
        public async Task<UserUpdateDTO> GetUserForUpdateAsync(string UserId)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(UserId);

            return mapper.Map<UserUpdateDTO>(user);
        }

        public async Task<IdentityResult> UpdateUserAsync(string UserId, UserUpdateDTO userParam)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(UserId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (!string.IsNullOrWhiteSpace(userParam.UserName) && userParam.UserName != user.UserName)
            {
                user.UserName = userParam.UserName;
                user.NormalizedUserName = unitOfWork.UserManager.NormalizeName(user.UserName);
            }

            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
            {
                user.FirstName = userParam.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
            {
                user.LastName = userParam.LastName;
            }

            if (!string.IsNullOrWhiteSpace(userParam.Email) && userParam.Email != user.Email)
            {
                user.Email = userParam.Email;
                user.NormalizedEmail = unitOfWork.UserManager.NormalizeEmail(user.Email);
            }

            if (!string.IsNullOrWhiteSpace(userParam.PhotoPath) && userParam.PhotoPath != user.PhotoPath)
            {
                user.PhotoPath = userParam.PhotoPath;
            }

            if (!string.IsNullOrEmpty(userParam.NewPassword))
            {
                unitOfWork.UserManager.PasswordHasher.HashPassword(user, userParam.NewPassword);
            }

            var result = await unitOfWork.UserManager.UpdateAsync(user);

            return result;
        }

        public async Task DeleteUserAsync(string Id)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(Id);

            if (user != null)
            {
                await unitOfWork.UserManager.DeleteAsync(user);
            }
        }

        public async Task<bool> IsEmailUniqueAsync(string Email)
        {
            var user = await unitOfWork.UserManager.FindByEmailAsync(Email);

            if (user == null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> IsUserNameUniqueAsync(string userName)
        {
            var user = await unitOfWork.UserManager.FindByNameAsync(userName);

            if (user == null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> CheckUserPasswordAsync(UserDTO userDTO, string password)
        {
            var user = mapper.Map<User>(userDTO);

            return await unitOfWork.UserManager.CheckPasswordAsync(user, password);
        }
    }
}
