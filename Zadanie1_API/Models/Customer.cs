using System.ComponentModel.DataAnnotations;

namespace Zadanie1_API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        [MaxLength(10)]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Use just digits")]
        public string NIP { get; set; }
    }
}
