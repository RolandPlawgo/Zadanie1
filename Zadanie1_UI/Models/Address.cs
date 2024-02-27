using System.Runtime.CompilerServices;
using System.Text;

namespace Zadanie1_UI.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Street);
            stringBuilder.Append(' ');
            stringBuilder.Append(HouseNumber);
            stringBuilder.Append(", ");
            stringBuilder.Append(ZipCode);
            stringBuilder.Append(' ');
            stringBuilder.Append(City);
            stringBuilder.Append(", ");
            stringBuilder.Append(Country);
            return stringBuilder.ToString();
        }
    }
}