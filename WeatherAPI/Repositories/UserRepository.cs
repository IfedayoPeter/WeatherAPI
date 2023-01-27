namespace WeatherAPI.Repositories
{
    public class UserRepository
    {
        public static readonly List<User> Users = new()
        {
            new User
            {Username= "Admin_1", Email= "admin@user.com", FirstName= "Jon",
                LastName="Doe", Password= "ADMIN", Role="Administrator"},

            new User(username: "Karen41", email: "karen@user.com", firstName: "Karen", lastName: "Jones", password: "KAREN",
                role: "Standard")
        };
    }
}
