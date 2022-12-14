using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using TP2_14E_A2022.Data;
using TP2_14E_A2022.DataModels;
using TP2_14E_A2022.Lots;
using TP2_14E_A2022.Utils;

namespace TP2_14E_A2022.Users
{
    public static class UserSystem
    {
        public static IUserDAL userDal;

        static UserSystem()
        {
            userDal = new UserDAL();
        }
        public static string GetConnectedUserName()
        {
            if (ApplicationState.ConnectedUser != null)
            {
                return ApplicationState.ConnectedUser.firstName;
            }
            return string.Empty;
        }

        public static User? AddUser(string email, string pwd, string firstName, 
            string lastName, string address, string telephone, DateTime subscriptionEnd, bool isAdmin)
        {
            if (!Validate(email, pwd, firstName, lastName, address, telephone))
            {
                return null;
            }
            else
            {
                string hashPwd = CryptingUtils.GetStringSha256Hash(pwd);
                User user = new User(email, hashPwd, firstName, lastName, address, 
                    telephone, subscriptionEnd, isAdmin);
                return userDal.AddUser(user);
            }
        }

        public static List<User> GetUsers()
        {
            return userDal.GetUsers();
        }

        public static List<User> GetSubscribedUsers()
        {
            return userDal.GetSubscribedUsers();
        }

        public static User? GetUser(ObjectId userId)
        {
            return userDal.GetUser(userId);
        }

        public static User? UpdateUser(User user)
        {
            if (!Validate(user.email, user.hashPwd, user.firstName, user.lastName, user.address, user.telephone))
            {
                return null;
            }
            return userDal.UpdateUser(user);
        }

        public static bool DeleteUser(object user)
        {
            if (user is User userToDelete)
            {
                List<Lot> lots = LotSystem.GetLotsOwnedBy(userToDelete.id);
                if(lots != null)
                {
                    foreach (Lot lot in lots)
                    {
                        LotSystem.DeActivateLot(lot.lotNumber);
                    }
                }
                return userDal.DeleteUser(userToDelete);
            }
            return false;
        }

        private static bool Validate(string email, string pwd, string firstName, string lastName, string address, string telephone)
        {
            bool conditionsEmail = email.Contains("@") && email.Contains(".");
            bool conditionsTelephone = telephone.Length == 10 && telephone.All(char.IsDigit);

            if (AreNotEmpty(email, pwd, firstName, lastName, address, telephone) && conditionsEmail && conditionsTelephone)
            {
                return true;
            }
            return false;
        }
        private static bool AreNotEmpty(string email, string pwd, string firstName, string lastName, string address, string telephone)
        {
            return email != string.Empty && pwd != string.Empty && firstName != string.Empty && lastName != string.Empty && address != string.Empty && telephone != string.Empty;
        }
    }
}
