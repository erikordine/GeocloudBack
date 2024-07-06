using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    public class UnitDto
    {
        //Id
        [ Required(ErrorMessage = "{0} is required") ]
        public int Id { get; set; }

        //TypeId
        [ Required(ErrorMessage = "{0} is required") ]
        public int TypeId { get; set; }

        //Type
        public UnitTypeDto? Type { get; set; }

        //Name
        [ Required(ErrorMessage = "{0} is required") ]
        [ MaxLength(20, ErrorMessage = "{0} must have a maximum of 20 characters") ]
        public string? Name { get; set; }
    }
}