using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Application.Services;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Repositories;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


namespace GeoCloudAI.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddCors();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "GeoCloudAI.API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using Bearer. Ex: 'Bearer 1234abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            services.AddScoped<DbSession>();

            //Token ***********************************************************

            var key = Encoding.ASCII.GetBytes(Configuration["TokenKey"].ToString());
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //Services ********************************************************

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICountryRepository, CountryRepository>();

            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddScoped<ICompanyTypeService, CompanyTypeService>();
            services.AddScoped<ICompanyTypeRepository, CompanyTypeRepository>();

            services.AddScoped<ICoreShedService, CoreShedService>();
            services.AddScoped<ICoreShedRepository, CoreShedRepository>();

            services.AddScoped<IDepositService, DepositService>();
            services.AddScoped<IDepositRepository, DepositRepository>();

            services.AddScoped<IDepositTypeService, DepositTypeService>();
            services.AddScoped<IDepositTypeRepository, DepositTypeRepository>();

            services.AddScoped<IDrillBoxService, DrillBoxService>();
            services.AddScoped<IDrillBoxRepository, DrillBoxRepository>();

            services.AddScoped<IDrillBoxActivityTypeService, DrillBoxActivityTypeService>();
            services.AddScoped<IDrillBoxActivityTypeRepository, DrillBoxActivityTypeRepository>();

            services.AddScoped<IDrillBoxStatusService, DrillBoxStatusService>();
            services.AddScoped<IDrillBoxStatusRepository, DrillBoxStatusRepository>();

            services.AddScoped<IDrillBoxTypeService, DrillBoxTypeService>();
            services.AddScoped<IDrillBoxTypeRepository, DrillBoxTypeRepository>();

            services.AddScoped<IDrillBoxMaterialService, DrillBoxMaterialService>();
            services.AddScoped<IDrillBoxMaterialRepository, DrillBoxMaterialRepository>();

            services.AddScoped<IDrillHoleService, DrillHoleService>();
            services.AddScoped<IDrillHoleRepository, DrillHoleRepository>();

            services.AddScoped<IDrillHoleRunService, DrillHoleRunService>();
            services.AddScoped<IDrillHoleRunRepository, DrillHoleRunRepository>();

            services.AddScoped<IDrillHoleTypeService, DrillHoleTypeService>();
            services.AddScoped<IDrillHoleTypeRepository, DrillHoleTypeRepository>();

            services.AddScoped<IDrillingTypeService, DrillingTypeService>();
            services.AddScoped<IDrillingTypeRepository, DrillingTypeRepository>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IEmployeeRoleService, EmployeeRoleService>();
            services.AddScoped<IEmployeeRoleRepository, EmployeeRoleRepository>();

            services.AddScoped<IFunctionalityService, FunctionalityService>();
            services.AddScoped<IFunctionalityRepository, FunctionalityRepository>();
            
            services.AddScoped<IFunctionalityTypeService, FunctionalityTypeService>();
            services.AddScoped<IFunctionalityTypeRepository, FunctionalityTypeRepository>();

            services.AddScoped<IMetalGroupService, MetalGroupService>();
            services.AddScoped<IMetalGroupRepository, MetalGroupRepository>();

            services.AddScoped<IMetalGroupSubService, MetalGroupSubService>();
            services.AddScoped<IMetalGroupSubRepository, MetalGroupSubRepository>();

            services.AddScoped<IMineService, MineService>();
            services.AddScoped<IMineRepository, MineRepository>();

            services.AddScoped<IMineSizeService, MineSizeService>();
            services.AddScoped<IMineSizeRepository, MineSizeRepository>();

            services.AddScoped<IMineStatusService, MineStatusService>();
            services.AddScoped<IMineStatusRepository, MineStatusRepository>();

            services.AddScoped<IMineAreaService, MineAreaService>();
            services.AddScoped<IMineAreaRepository, MineAreaRepository>();

            services.AddScoped<IMineAreaTypeService, MineAreaTypeService>();
            services.AddScoped<IMineAreaTypeRepository, MineAreaTypeRepository>();

            services.AddScoped<IMineAreaStatusService, MineAreaStatusService>();
            services.AddScoped<IMineAreaStatusRepository, MineAreaStatusRepository>();

            services.AddScoped<IMineAreaShapeService, MineAreaShapeService>();
            services.AddScoped<IMineAreaShapeRepository, MineAreaShapeRepository>();

            services.AddScoped<IOreGeneticTypeService, OreGeneticTypeService>();
            services.AddScoped<IOreGeneticTypeRepository, OreGeneticTypeRepository>();

            services.AddScoped<IOreGeneticTypeSubService, OreGeneticTypeSubService>();
            services.AddScoped<IOreGeneticTypeSubRepository, OreGeneticTypeSubRepository>();
            
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IProfileRepository, ProfileRepository>();

            services.AddScoped<IProfileRoleService, ProfileRoleService>();
            services.AddScoped<IProfileRoleRepository, ProfileRoleRepository>();

            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            services.AddScoped<IProjectStatusService, ProjectStatusService>();
            services.AddScoped<IProjectStatusRepository, ProjectStatusRepository>();

            services.AddScoped<IProjectTypeService, ProjectTypeService>();
            services.AddScoped<IProjectTypeRepository, ProjectTypeRepository>();

            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IRegionRepository, RegionRepository>();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IUnitRepository, UnitRepository>();

            services.AddScoped<IUnitTypeService, UnitTypeService>();
            services.AddScoped<IUnitTypeRepository, UnitTypeRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            var dir = "Resources";
            if (!Directory.Exists(dir)) 
            {
                Directory.CreateDirectory(dir);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeoCloudAI.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(
                c => c.AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowAnyOrigin()
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions() {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}