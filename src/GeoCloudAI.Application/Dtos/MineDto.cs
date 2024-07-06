using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    public class MineDto
    {
        //Id
        [ Required(ErrorMessage = "{0} is required") ]
        public int Id { get; set; }

        //DepositId
        [ Required(ErrorMessage = "{0} is required") ]
        public int DepositId { get; set; }

        //Deposit
        public DepositDto? Deposit { get; set; }
        
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

        //SizeId
        public int? SizeId { get; set; }

        //Size
        public MineSizeDto? Size { get; set; }

        //StatusId
        public int? StatusId { get; set; }

        //Status
        public MineStatusDto? Status { get; set; }

        //StatusPreviousId
        public int? StatusPreviousId { get; set; }

        //StatusPrevious
        public MineStatusDto? StatusPrevious { get; set; }

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

        //QttMineAreas
        public int? QttMineAreas { get; set; }

        //QttDrillHoles
        public int? QttDrillHoles { get; set; }

        //QttDrillBoxes
        public int? QttDrillBoxes { get; set; }
    }
}