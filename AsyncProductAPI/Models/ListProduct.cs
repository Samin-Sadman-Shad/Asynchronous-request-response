using System.ComponentModel.DataAnnotations;

namespace AsyncProductAPI.Models
{
    public class ListProduct
    {
        [Key]
        public int Id { get; set; }

        public string? PorductDetails { get; set; }

        public string? RequestStatus { get; set; }
    }
}
