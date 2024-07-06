using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    public class DrillingTypeDto
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
        public string Name { get; set; }

        //ImgType
        [ MaxLength(4, ErrorMessage = "{0} must have a maximum of 4 characters") ]
        public string? ImgType { get; set; }
    }
}