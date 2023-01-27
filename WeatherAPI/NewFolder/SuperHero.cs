using System.ComponentModel.DataAnnotations;

namespace WeatherAPI
{
    public class SuperHero
    {
        [Key] public int Id { get; set; } 
        public String Name { get; set; } = String.Empty;
        public String FirstName { get; set; } = String.Empty;
        public String LastName { get; set; } = String.Empty;
        public String Place { get; set; } = String.Empty;
        public String Nickname { get; set; } = String.Empty;

    }
}
