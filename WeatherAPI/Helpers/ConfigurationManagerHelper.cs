namespace WeatherAPI.Helpers
{
    public static class ConfigurationManagerHelper
    {
        public static IConfiguration AppSettings
        {
            get;
        }
        static ConfigurationManagerHelper()
        {
            AppSettings = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }

        public const string WeatherHttpApi = "WeatherAPI";
    }
}
