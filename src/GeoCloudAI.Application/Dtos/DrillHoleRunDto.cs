using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    public class DrillHoleRunDto
    {
        //Id
        [ Required(ErrorMessage = "{0} is required") ]
        public int Id { get; set; }

        //DrillHoleId
        [ Required(ErrorMessage = "{0} is required") ]
        public int DrillHoleId { get; set; }

        //DrillHole
        public DrillHoleDto? DrillHole { get; set; }

        //StartDepth
        public double? StartDepth { get; set; }

        //EndDepth
        public double? EndDepth { get; set; }

        //StartTime
        public DateTime? StartTime { get; set; }

        //EndTime
        public DateTime? EndTime { get; set; }

        //UserId 
        [ Required(ErrorMessage = "{0} is required") ]
        public int UserId { get; set; }

        //User 
        public UserDto? User { get; set; }

        //Register
        [ Required(ErrorMessage = "{0} is required") ]
        public DateTime? Register { get; set; }
    }
}