using System.ComponentModel.DataAnnotations;

namespace Afri_Energy_Connect.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [MaxLength(100)]
        public string FarmerID { get; set; } = "";

        [MaxLength(100)]
        public string ProductName { get; set; } = "";

        [MaxLength(100)]
        public string ProductCategory { get; set; } = "";
       
        public DateTime ProductionDate { get; set; }


    }
}
