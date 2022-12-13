using MongoDB.Bson;
using System.Collections.Generic;
using TP2_14E_A2022.DataModels;

namespace TP2_14E_A2022.Data
{
    public interface IUserDAL
    {
        User? ConnectUser(string email, string hashPwd);
        List<User> GetUsers();
        List<User> GetSubscribedUsers();
        User? GetUser(ObjectId userId);
        User AddUser(User user);
        User UpdateUser(User user);
        bool DeleteUser(User user);
    }
}