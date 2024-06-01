using System.ComponentModel.DataAnnotations;

namespace Afri_Energy_Connect.Models
{
    public class ProductDto
    {
        [Required, MaxLength(100)]
        public string ProductName { get; set; } = "";


        [Required, MaxLength(100)]
        public string ProductCategory { get; set; } = "";

        public DateTime ProductionDate { get; set; }

    }
}
