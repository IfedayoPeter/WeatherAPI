namespace WeatherAPI
{
    public class User
    {
        public User()
        {
        }

        public User(string username, string email, string firstName, string lastName, string password, string role)
        {
            Username = username;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Role = role;
        }

        public String Username { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Role { get; set; }

    }
}
