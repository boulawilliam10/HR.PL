using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.DAL.Models
{
	[PrimaryKey("EmployeeId", "ProjectId")]
	public class Work_For
    {
        //Employee And Work_For RelationShip => One 
        [ForeignKey("Employee")]

        public int? EmployeeId { get; set; }

        [InverseProperty("Works")]
        public Employee? Employee { get; set; }
        //--------------------------------
        //Employee And Work_For RelationShip => One 
        [ForeignKey("Project")]
        public int? ProjectId { get; set; }

        [InverseProperty("Works")]
        public Project? Project { get; set; }
        //--------------------------------
        public int NumOfHours { get; set; }
    }
}
