using System.Collections.Generic;

namespace APICarData.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "example_admin",         Password = "MyPass_w0rd",  Role = "Administrator" },
            new UserModel() { Username = "example_general_user1", Password = "MyPass_w0rd",  Role = "GeneralUser" },
            new UserModel() { Username = "example_general_user2", Password = "MyPass_w0rd2", Role = "GeneralUser" },
        };
    }
}