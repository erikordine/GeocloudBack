using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    public class DepositDto
    {
        //Id
        [ Required(ErrorMessage = "{0} is required") ]
        public int Id { get; set; }

        //RegionId
        [ Required(ErrorMessage = "{0} is required") ]
        public int RegionId { get; set; }

        //Region
        public RegionDto? Region { get; set; }
        
        //Name
        [ Required(ErrorMessage = "{0} is required") ]
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Name { get; set; }

        //AlternativeNames
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? AlternativeNames { get; set; }

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
        
        //GeologicalDistrict
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? GeologicalDistrict { get; set; }

        //DiscoveryBy
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? DiscoveryBy { get; set; }

        //DiscoveryYear
        public int? DiscoveryYear { get; set; }

        //Resource
        public int? Resource { get; set; }

        //Reserve
        public int? Reserve { get; set; }

        //Comments
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Comments { get; set; }

        //DepositTypeId
        public int? DepositTypeId { get; set; }

        //DepositType
        public DepositTypeDto? DepositType { get; set; }

        //OreGeneticTypeId
        public int? OreGeneticTypeId { get; set; }

        //OreGeneticType
        public OreGeneticTypeDto? OreGeneticType { get; set; }

        //OreGeneticTypeSubId
        public int? OreGeneticTypeSubId { get; set; }

        //OreGeneticTypeSub
        public OreGeneticTypeSubDto? OreGeneticTypeSub { get; set; }

        //MetalGroupId
        public int? MetalGroupId { get; set; }

        //MetalGroup
        public MetalGroupDto? MetalGroup { get; set; }

        //MetalGroupSubId
        public int? MetalGroupSubId { get; set; }

        //MetalGroupSub
        public MetalGroupSubDto? MetalGroupSub { get; set; }

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