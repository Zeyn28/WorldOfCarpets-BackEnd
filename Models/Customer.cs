using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class Customer
    {
        [Key]
        public long CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Mail { get; set; }
        public long Number { get; set; }
        public List<Measurement>? Measurements { get; set; }
    }
}
