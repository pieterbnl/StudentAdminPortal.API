using AutoMapper;
using StudentAdminPortal.API.DomainModels;
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
        }
    }
}