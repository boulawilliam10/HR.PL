using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.DAL.Models
{
    public class Project
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        //--------------------------------
        //Project And Department RelationShip => One 
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        [InverseProperty("Projects")]
        public Department? Department { get; set; }
        //--------------------------------
        //Project And Work_For RelationShip => Many 
        [InverseProperty("Project")]
        public ICollection<Work_For> Works { get; set; } = new HashSet<Work_For>();
    }
}
