using System.ComponentModel.DataAnnotations;

namespace Afri_Energy_Connect.Models
{
    public class FarmersDto
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Occupation is required")]
        [MaxLength(100, ErrorMessage = "Occupation cannot exceed 100 characters")]
        public string Occupation { get; set; } = "";

    }
}
    

