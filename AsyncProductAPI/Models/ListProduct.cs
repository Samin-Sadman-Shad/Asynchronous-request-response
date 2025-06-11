using System.ComponentModel.DataAnnotations;

namespace AsyncProductAPI.Models
{
    public class ListProduct
    {
        [Key]
        public int Id { get; set; }

        public string? ProductDetails { get; set; }

        public string? RequestStatus { get; set; }

        public Guid RequestId { get; set; }
    }
}
