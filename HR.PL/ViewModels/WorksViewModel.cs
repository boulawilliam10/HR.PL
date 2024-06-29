using HR.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.PL.ViewModels
{
    [PrimaryKey("EmployeeId", "ProjectId")]
    public class WorksViewModel
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
