using System.ComponentModel.DataAnnotations;

namespace Zadanie1_API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "NIP must consist of 10 digits")]
        public string NIP { get; set; }
    }
}
