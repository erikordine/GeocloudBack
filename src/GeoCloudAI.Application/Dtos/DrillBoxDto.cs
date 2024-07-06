using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    public class DrillBoxDto
    {
        //Id
        [ Required(ErrorMessage = "{0} is required") ]
        public int Id { get; set; }

        //DrillHoleId
        [ Required(ErrorMessage = "{0} is required") ]
        public int DrillHoleId { get; set; }

        //DrillHole
        public DrillHoleDto? DrillHole { get; set; }

        //Number
        [ Required(ErrorMessage = "{0} is required") ]
        public int Number { get; set; }

        //AmountCores
        [ Range(1, 10, ErrorMessage = "{0} must have a value between 1 and 10")]
        public int? AmountCores { get; set; }

        //Code
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Code { get; set; }

        //Uuid
        [ MaxLength(50, ErrorMessage = "{0} must have a maximum of 50 characters") ]
        public string? Uuid { get; set; }

        //StartDepth
        public double? StartDepth { get; set; }

        //EndDepth
        public double? EndDepth { get; set; }

        //Description
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Description { get; set; }

        //Comments
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Comments { get; set; }

        //TypeId
        public int? TypeId { get; set; }

        //Type
        public DrillBoxTypeDto? Type { get; set; }

        //StatusId
        public int? StatusId { get; set; }

        //Status
        public DrillBoxStatusDto? Status { get; set; }

        //MaterialId
        public int? MaterialId { get; set; }

        //Material
        public DrillBoxMaterialDto? Material { get; set; }

        //CoreShedId
        public int? CoreShedId { get; set; }

        //CoreShed
        public CoreShedDto? CoreShed { get; set; }

        //Shelves
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Shelves { get; set; }

        //ImgType
        [ MaxLength(4, ErrorMessage = "{0} must have a maximum of 4 characters") ]
        public string? ImgType { get; set; }
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