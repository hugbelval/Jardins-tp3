using TP2_14E_A2022.DataModels;
using TP2_14E_A2022.Utils;

namespace TP2_14E_A2022_Tests.Users
{
    public static class UserGenerator
    {

        public static string originalPwd = "test";
        public static User GenerateUser()
        {
            return new User("email@test.com", CryptingUtils.GetStringSha256Hash(originalPwd),
                "firstName", "lastName", "address", "5819997777", DateTime.Now, false);
        }

        public static User GenerateUserWithIncorrectPhoneNumber()
        {
            return new User("email@test.com", CryptingUtils.GetStringSha256Hash(originalPwd),
                "firstName", "lastName", "address", "incorrectPhone", DateTime.Now, true);
        }

        public static User GenerateUserWithIncorrectEmail()
        {
            return new User("incorrectEmail", CryptingUtils.GetStringSha256Hash(originalPwd),
                "firstName", "lastName", "address", "5819997777", DateTime.Now, true);
        }

        public static User GenerateUserWithEmptyName()
        {
            return new User("incorrectEmail", CryptingUtils.GetStringSha256Hash(originalPwd),
                string.Empty, string.Empty, "address", "5819997777", DateTime.Now, true);
        }
    }
}
