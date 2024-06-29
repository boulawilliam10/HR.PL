using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
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
