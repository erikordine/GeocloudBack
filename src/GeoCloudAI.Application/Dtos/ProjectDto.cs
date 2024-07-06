using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    public class ProjectDto
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

        //StartDate
        public DateTime? StartDate { get; set; }

        //EndDate
        public DateTime? EndDate { get; set; }

        //Summary
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Summary { get; set; }

        //Comments
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Comments { get; set; }

        //TypeId
        public int? TypeId { get; set; }

        //Type
        public ProjectTypeDto? Type { get; set; }

        //StatusId
        public int? StatusId { get; set; }

        //Status
        public ProjectStatusDto? Status { get; set; }
       
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