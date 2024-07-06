using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    public class DrillHoleTypeDto
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
        [ MinLength(2, ErrorMessage = "{0} must have at least 2 characters") ]
        [ MaxLength(20, ErrorMessage = "{0} must have a maximum of 20 characters") ]
        public string Name { get; set; }

        //Diameter
        [ Required(ErrorMessage = "{0} is required") ]
        [ Range(1, 1000, ErrorMessage = "{0} must have a value between 1 and 1000")]
        public int Diameter { get; set; }
    }
}