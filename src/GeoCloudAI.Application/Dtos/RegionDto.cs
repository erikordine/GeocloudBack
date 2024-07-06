using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    public class RegionDto
    {
        //Id
        [ Required(ErrorMessage = "{0} is required") ]
        public int Id { get; set; }

        //AccountId
        [ Required(ErrorMessage = "{0} is required") ]
        public int AccountId { get; set; }

        //Account
        public AccountDto? Account { get; set; }
        
        //Name
        [ Required(ErrorMessage = "{0} is required") ]
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Name { get; set; }
       
        //CountryId
        [ Required(ErrorMessage = "{0} is required") ]
        public int CountryId { get; set; }

        //Country
        public CountryDto? Country { get; set; }

        //State
        [ MinLength(2, ErrorMessage = "{0} must have at least 2 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? State { get; set; }

        //City
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? City { get; set; }
        
        //Latitude
        public double? Latitude { get; set; }
        
        //Longitude
        public double? Longitude { get; set; }
        
        //Comments
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Comments { get; set; }

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

        //QttDeposits
        public int? QttDeposits { get; set; }

        //QttMines
        public int? QttMines { get; set; }

        //QttMineAreas
        public int? QttMineAreas { get; set; }

        //QttDrillHoles
        public int? QttDrillHoles { get; set; }

        //QttDrillBoxes
        public int? QttDrillBoxes { get; set; }

    }
}