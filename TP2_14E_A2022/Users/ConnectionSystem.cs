using System;
using System.Windows;
using TP2_14E_A2022.Data;
using TP2_14E_A2022.DataModels;
using TP2_14E_A2022.Utils;

namespace TP2_14E_A2022.Users
{
    public static class ConnectionSystem
    {
        public static IUserDAL dalClient;

        static ConnectionSystem()
        {
            dalClient = new UserDAL();
        }

        public static bool ConnectUser(string email, string pwd)
        {
            string hashPwd = CryptingUtils.GetStringSha256Hash(pwd);
            try 
            {
                User user = dalClient.ConnectUser(email, hashPwd);
                if (user != null)
                {
                    ApplicationState.ConnectedUser = user;
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Une erreur est survenue avec la base de données", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;
        }
        
    }
}
