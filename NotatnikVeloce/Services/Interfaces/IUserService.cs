﻿using NotatnikVeloce.Models;

namespace NotatnikVeloce.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
        User? GetUser(Guid id);
        byte[] GetRaportInExcel();
        byte[] GetRaportInPdf();
        User? AddUser(UserDto userDto);
        User? UpdateUser(Guid id, UserDto userDto);
        bool DeleteUser(Guid id);
    }
}
