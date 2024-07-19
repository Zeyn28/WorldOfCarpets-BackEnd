using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoApi.Models;

namespace ToDoAPI.Models
{
    public class Measurement
    {
        [Key]
        public long MeasurementId { get; set; }

        [ForeignKey(nameof(Employee))]
        public long EmployeeId { get; set; }
        public virtual Employee Employee { get; set; } = null!;

        [ForeignKey(nameof(Customer))]
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!; //virtual:override yapabiliyosun
        public float Width { get; set; }
        public float Height { get; set; }
        public float TotalPrice { get; set; }

        
        //date??

    }
}
