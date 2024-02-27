using System.ComponentModel.DataAnnotations;

namespace Zadanie1_API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Address Address { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "NIP must consist of 10 digits")]
        public string NIP { get; set; }
    }
}
