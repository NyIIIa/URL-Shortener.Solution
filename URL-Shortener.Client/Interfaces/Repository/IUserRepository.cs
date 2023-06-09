﻿using URL_Shortener.Client.Models.Entities;

namespace URL_Shortener.Client.Interfaces.Repository;

public interface IUserRepository : IRepository<User>
{
    bool IsUserExist(string login);
    User GetUserByLogin(string login);
}