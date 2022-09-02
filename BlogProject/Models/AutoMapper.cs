using AutoMapper;

namespace BlogProject.Models
{
    public class AutoMapper:Profile
    {
            public AutoMapper()
            {
                CreateMap<Account, AppUser>()
                    .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            }
        }
    }

