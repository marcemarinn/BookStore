﻿using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IUserRepository
{
    Task<int> Register(UserDto request);
    Task<int> Login(UserDto request);
    public string CreateToken(User user);



}
