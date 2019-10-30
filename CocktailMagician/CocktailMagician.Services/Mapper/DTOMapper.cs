using System;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;

namespace CocktailMagician.Services.Mapper
{
    public static class DTOMapper
    {
        public static UserDTO MapToDTO(this User user)
        {
            var dto = new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                RoleName = user.Role.Name,
                Password = user.Password

            };
            if (user.Bann != null)
            {
                dto.BannReason = user.Bann?.Reason;
                dto.BannEndTime = user.Bann.EndDateTime;
                dto.BannId = user.Bann.Id;
            }

            return dto;
        }
        public static BannDTO MapToDTO(this Bann bann)
        {
            var dto = new BannDTO
            {
                Id = bann.Id,
                Reason = bann.Reason,
                EndDateTime = bann.EndDateTime

            };
            if (bann.User != null)
            {
                dto.UserId = bann.UserId;
                dto.UserId = bann.User.UserName;
            }

            return dto;
        }
    }
}
