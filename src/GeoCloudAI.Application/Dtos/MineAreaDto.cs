using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    public class MineAreaDto
    {
        //Id
        [ Required(ErrorMessage = "{0} is required") ]
        public int Id { get; set; }

        //MineId
        [ Required(ErrorMessage = "{0} is required") ]
        public int MineId { get; set; }

        //Mine
        public MineDto? Mine { get; set; }
        
        //Name
        [ Required(ErrorMessage = "{0} is required") ]
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Name { get; set; }
       
        //Latitude
        public double? Latitude { get; set; }
        
        //Longitude
        public double? Longitude { get; set; }
        
        //StartYear
        public int? StartYear { get; set; }

        //EndYear
        public int? EndYear { get; set; }

        //Resource
        public int? Resource { get; set; }

        //Reserve
        public int? Reserve { get; set; }

        //OreMined
        public int? OreMined { get; set; }

        //Comments
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Comments { get; set; }

        //TypeId
        public int? TypeId { get; set; }

        //Type
        public MineAreaTypeDto? Type { get; set; }

        //StatusId
        public int? StatusId { get; set; }

        //Status
        public MineAreaStatusDto? Status { get; set; }

        //ShapeId
        public int? ShapeId { get; set; }

        //Shape
        public MineAreaShapeDto? Shape { get; set; }

        //ImgTypeProfile
        [ MaxLength(4, ErrorMessage = "{0} must have a maximum of 4 characters") ]
        public string? ImgTypeProfile { get; set; }

        //ImgTypeCover
        [ MaxLength(4, ErrorMessage = "{0} must have a maximum of 4 characters") ]
        public string? ImgTypeCover { get; set; }

        //UserId 
        [ Required(ErrorMessage = "{0} is required") ]
        public int UserId { get; set; }

        //User 
        public UserDto? User { get; set; }

        //Register
        [ Required(ErrorMessage = "{0} is required") ]
        public DateTime? Register { get; set; }

        //QttDrillHoles
        public int? QttDrillHoles { get; set; }

        //QttDrillBoxes
        public int? QttDrillBoxes { get; set; }
    }
}