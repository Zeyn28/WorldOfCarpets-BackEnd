using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ToDoAPI.Models;

namespace TodoApi.Models
{
    public class Employee
    {
        [Key]
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string Mail { get; set; } = null!;
        public string Password { get; set; } = null!;
        public long Number { get; set; }

        public List<Measurement>? Measurements { get; set; }
    }
}


