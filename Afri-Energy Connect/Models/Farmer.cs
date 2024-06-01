using System.ComponentModel.DataAnnotations;

namespace Afri_Energy_Connect.Models
{
    public class Farmer
    {
        public int ID { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = "";

        [MaxLength(100)]
        public string Email { get; set; } = "";

        [MaxLength(100)]
        public string Password { get; set; } = "";

        public string Occupation { get; set; } = "";

    }
}
