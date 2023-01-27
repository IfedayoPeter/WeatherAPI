namespace WeatherAPI.services
{
    public interface IUserService
    {
        public User Get(UserLogin userLogin);
        public string Login(UserLogin userLogin);
    }
}
