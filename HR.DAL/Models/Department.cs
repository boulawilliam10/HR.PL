using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.DAL.Models
{
    public class Department
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
