using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    public class DrillBoxActivityDto
    {
        //Id
        [ Required(ErrorMessage = "{0} is required") ]
        public int Id { get; set; }

        //DrillBoxId
        [ Required(ErrorMessage = "{0} is required") ]
        public int DrillBoxId { get; set; }

        //DrillBox
        public DrillBoxDto? DrillBox { get; set; }
       
        //ActivityId
        public int? ActivityId { get; set; }

        //Activity
        public DrillBoxActivityTypeDto? Activity { get; set; }

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