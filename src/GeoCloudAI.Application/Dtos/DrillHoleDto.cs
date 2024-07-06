using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    public class DrillHoleDto
    {
        //Id
        [ Required(ErrorMessage = "{0} is required") ]
        public int Id { get; set; }

        //RegionId
        [ Required(ErrorMessage = "{0} is required") ]
        public int RegionId { get; set; }

        //Region
        public RegionDto? Region { get; set; }

        //DepositId
        public int? DepositId { get; set; }

        //Deposit
        public DepositDto? Deposit { get; set; }

        //MineId
        public int? MineId { get; set; }

        //Mine
        public MineDto? Mine { get; set; }

        //MineAreaId
        public int? MineAreaId { get; set; }

        //MineArea
        public MineAreaDto? MineArea { get; set; }

        //Name
        [ Required(ErrorMessage = "{0} is required") ]
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string Name { get; set; }

        //Latitude
        public double? Latitude { get; set; }
        
        //Longitude
        public double? Longitude { get; set; }

        //Elevation
        public double? Elevation { get; set; }

        //Length 
        public int? Length { get; set; }

        //Comments
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Comments { get; set; }
        
        //TypeId
        public int? TypeId { get; set; }

        //Type
        public DrillHoleTypeDto? Type { get; set; }

        //DrillingTypeId
        public int? DrillingTypeId { get; set; }

        //DrillingType
        public DrillingTypeDto? DrillingType { get; set; }

        //ContractorId
        public int? ContractorId { get; set; }

        //Contractor
        public CompanyDto? Contractor { get; set; }

        //DrillerId
        public int? DrillerId { get; set; }

        //Driller
        public CompanyDto? Driller { get; set; }

        //StartDate
        public DateTime? StartDate { get; set; }

        //EndDate
        public DateTime? EndDate { get; set; }

        //UserId 
        [ Required(ErrorMessage = "{0} is required") ]
        public int UserId { get; set; }

        //User 
        public UserDto? User { get; set; }

        //Register
        [ Required(ErrorMessage = "{0} is required") ]
        public DateTime? Register { get; set; }

        //QttDrillBoxes
        public int? QttDrillBoxes { get; set; }
    }
}