using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Domain.Classes;

namespace GeoCloudAI.Application.Helpers
{
    public class GeoCloudAIProfile : AutoMapper.Profile
    {
        public GeoCloudAIProfile()
        {
            CreateMap<Account,               AccountDto>().ReverseMap();
            CreateMap<Company,               CompanyDto>().ReverseMap();
            CreateMap<CompanyType,           CompanyTypeDto>().ReverseMap();
            CreateMap<CoreShed,              CoreShedDto>().ReverseMap();
            CreateMap<Country,               CountryDto>().ReverseMap();
            CreateMap<Deposit,               DepositDto>().ReverseMap();
            CreateMap<DepositType,           DepositTypeDto>().ReverseMap();
            CreateMap<DrillBox,              DrillBoxDto>().ReverseMap();
            CreateMap<DrillBoxStatus,        DrillBoxStatusDto>().ReverseMap();
            CreateMap<DrillBoxType,          DrillBoxTypeDto>().ReverseMap();
            CreateMap<DrillBoxMaterial,      DrillBoxMaterialDto>().ReverseMap();
            CreateMap<DrillBoxActivityType,  DrillBoxActivityTypeDto>().ReverseMap();
            CreateMap<DrillHole,             DrillHoleDto>().ReverseMap();
            CreateMap<DrillHoleRun,          DrillHoleRunDto>().ReverseMap();
            CreateMap<DrillHoleType,         DrillHoleTypeDto>().ReverseMap();
            CreateMap<DrillingType,          DrillingTypeDto>().ReverseMap();
            CreateMap<Employee,              EmployeeDto>().ReverseMap();
            CreateMap<EmployeeRole,          EmployeeRoleDto>().ReverseMap();
            CreateMap<Functionality,         FunctionalityDto>().ReverseMap();
            CreateMap<FunctionalityType,     FunctionalityTypeDto>().ReverseMap();
            CreateMap<LithologyGroup,        LithologyGroupDto>().ReverseMap();
            CreateMap<MetalGroup,            MetalGroupDto>().ReverseMap();
            CreateMap<MetalGroupSub,         MetalGroupSubDto>().ReverseMap();
            CreateMap<Mine,                  MineDto>().ReverseMap();
            CreateMap<MineSize,              MineSizeDto>().ReverseMap();
            CreateMap<MineStatus,            MineStatusDto>().ReverseMap();
            CreateMap<MineArea,              MineAreaDto>().ReverseMap();
            CreateMap<MineAreaType,          MineAreaTypeDto>().ReverseMap();
            CreateMap<MineAreaStatus,        MineAreaStatusDto>().ReverseMap();
            CreateMap<MineAreaShape,         MineAreaShapeDto>().ReverseMap();
            CreateMap<OreGeneticType,        OreGeneticTypeDto>().ReverseMap();
            CreateMap<OreGeneticTypeSub,     OreGeneticTypeSubDto>().ReverseMap();
            CreateMap<Profile,               ProfileDto>().ReverseMap();
            CreateMap<ProfileRole,           ProfileRoleDto>().ReverseMap();
            CreateMap<Project,               ProjectDto>().ReverseMap();
            CreateMap<ProjectStatus,         ProjectStatusDto>().ReverseMap();
            CreateMap<ProjectType,           ProjectTypeDto>().ReverseMap();
            CreateMap<Region,                RegionDto>().ReverseMap();
            CreateMap<Role,                  RoleDto>().ReverseMap();
            CreateMap<Unit,                  UnitDto>().ReverseMap();
            CreateMap<UnitType,              UnitTypeDto>().ReverseMap();
            CreateMap<User,                  UserDto   >().ReverseMap();
        }
    }
}