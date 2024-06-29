using HR.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HR.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [MaxLength(30,ErrorMessage =("The Name Is Required"))]
        public required string Name { get; set; }
        public decimal Salary { get; set; }
        [Phone] public string? PhoneNumber { get; set; }
        [EmailAddress] public string? Email { get; set; }
        //--------------------------------
        //Employee And Work_For RelationShip => Many 
        [InverseProperty("Employee")]
        public ICollection<Work_For> Works { get; set; } = new HashSet<Work_For>();
        //--------------------------------
        //Employee And Department RelationShip => One 
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        [InverseProperty("Employees")]
        public Department? Department { get; set; }
    }
}
