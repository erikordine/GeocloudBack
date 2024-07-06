using System.ComponentModel.DataAnnotations;

namespace GeoCloudAI.Application.Dtos
{
    public class ProfileRoleDto
    {
        //Id
        [ Required(ErrorMessage = "{0} is required") ]
        public int Id { get; set; }
        
        //ProfileId
        [ Required(ErrorMessage = "{0} is required") ]
        public int ProfileId { get; set; }

        //Profile
        public ProfileDto? Profile { get; set; }

        //RoleId
        [ Required(ErrorMessage = "{0} is required") ]
        public int RoleId { get; set; }

        //Role
        public RoleDto? Role { get; set; }
    }
}