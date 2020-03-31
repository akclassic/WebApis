using System.ComponentModel.DataAnnotations;


namespace SampleApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public string Designation { get; set; }
    }
}