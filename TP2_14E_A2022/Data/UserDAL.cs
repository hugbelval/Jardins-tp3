using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Windows;
using TP2_14E_A2022.DataModels;

namespace TP2_14E_A2022.Data
{
    public class UserDAL : DAL, IUserDAL
    {
        public User? ConnectUser(string email, string hashPwd)
        {
            return db.GetCollection<User>("Users").Find(x => x.email == email && x.hashPwd == hashPwd).SingleOrDefaultAsync<User>().Result;
        }

        public User? GetUser(ObjectId userId)
        {
            User? result = null;

            try
            {
                result = db.GetCollection<User>("Users").Find(x => x.id == userId).SingleOrDefaultAsync<User>().Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            try
            {
                users = db.GetCollection<User>("Users").Aggregate().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return users;
        }
        public User AddUser(User user)
        {
            try
            {
                IMongoCollection<User> users = this.db.GetCollection<User>("Users");
                users.InsertOne(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return user;
        }

        public User UpdateUser(User user)
        {
            try
            {
                IMongoCollection<User> users = this.db.GetCollection<User>("Users");
                FilterDefinition<User> filter = Builders<User>.Filter.Eq("id", user.id);
                UpdateDefinition<User> update = Builders<User>.Update.Set("email", user.email)
                                                  .Set("hashPwd", user.hashPwd)
                                                  .Set("firstName", user.firstName)
                                                  .Set("lastName", user.lastName)
                                                  .Set("address", user.address)
                                                  .Set("telephone", user.telephone)
                                                  .Set("subscriptionEnd", user.subscriptionEnd);
                users.UpdateOne(filter, update);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return user;
        }

        public bool DeleteUser(User user)
        {
            try
            {
                IMongoCollection<User> users = this.db.GetCollection<User>("Users");
                FilterDefinition<User> filter = Builders<User>.Filter.Eq("id", user.id);
                return users.DeleteOne(filter).DeletedCount > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

    }
}
