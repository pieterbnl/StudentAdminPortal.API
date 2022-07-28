using AutoMapper;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Profiles.AfterMaps;
using Datamodels = StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Profiles
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Datamodels.Student, Student>()
                .ReverseMap();

            CreateMap<Datamodels.Gender, Gender>()
                .ReverseMap();

            CreateMap<Datamodels.Address, Address>()
                .ReverseMap();

            // The following will map automatically as names match,
            // with the exception of Address (1 level down), which needs to be 'after' mapped 
            CreateMap<UpdateStudentRequest, Datamodels.Student>()
                .AfterMap<UpdateStudentRequestAfterMap>();
        }
    }
}