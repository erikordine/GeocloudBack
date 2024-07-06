using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    public class MetalGroupDto
    {
        //Id
        [ Required(ErrorMessage = "{0} is required") ]
        public int Id { get; set; }

        //OreGeneticTypeId
        [ Required(ErrorMessage = "{0} is required") ]
        public int OreGeneticTypeId { get; set; }

        //OreGeneticType
        public OreGeneticTypeDto? OreGeneticType { get; set; }

        //Name
        [ Required(ErrorMessage = "{0} is required") ]
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Name { get; set; }
    }
}