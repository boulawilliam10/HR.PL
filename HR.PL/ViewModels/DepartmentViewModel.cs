using HR.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        //--------------------------------
        // Department And Employees RelationShip => Many
        [InverseProperty("Department")]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        //--------------------------------
        // Department And Projects RelationShip => Many

        [InverseProperty("Department")]
        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
