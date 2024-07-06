using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    public class EmployeeDto
    {
        //Id
        [ Required(ErrorMessage = "{0} is required") ]
        public int Id { get; set; }

        //CompanyId
        [ Required(ErrorMessage = "{0} is required") ]
        public int CompanyId { get; set; }

        //Company
        public CompanyDto? Company { get; set; }
        
        //Name
        [ Required(ErrorMessage = "{0} is required") ]
        [ MinLength(4, ErrorMessage = "{0} must have at least 4 characters") ]
        [ MaxLength(40, ErrorMessage = "{0} must have a maximum of 40 characters") ]
        public string? Name { get; set; }
       
        //RoleId
        public int? RoleId { get; set; }

        //Role
        public EmployeeRoleDto? Role { get; set; }

        //ImgType
        [ MaxLength(4, ErrorMessage = "{0} must have a maximum of 4 characters") ]
        public string? ImgType { get; set; }

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