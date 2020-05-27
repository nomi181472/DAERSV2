using System.Linq;
using AutoMapper;
using DAERS.API.Dtos;
using DAERS.API.Dtos.DtosForExercise;
using DAERS.API.Dtos.DtosForNutritionFacts;
using DAERS.API.Models;

namespace DAERS.API.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User,UserForListDto>().ForMember(dest=>dest.PhotoUrl,opt=>{
                opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);
            });
            CreateMap<User,UserForDetailedDto>().ForMember(dest=>dest.PhotoUrl,opt=>{
                opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);
            });
            CreateMap<PhotoU,PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto,User>();
            CreateMap<PhotoForReturnDto,PhotoU>();
            CreateMap<PhotoForCreationDto,PhotoU>();
            CreateMap<UserForRegisterDto,User>();
            CreateMap<ExerciseForAddDto,Exercise>();
            CreateMap<PhotoE,PhotosEForDetailDto>();
            CreateMap<ExerciseForUpdateDto,Exercise>();
            CreateMap<Exercise,ExerciseForDetailDto>()
            .ForMember(dest=>dest.PhotoEUrl,opt=>{
            opt.MapFrom(src=>src.PhotosE.FirstOrDefault(p=>p.Url!=null).Url);
            });
            CreateMap<Exercise,ExerciseForListDto>()
            .ForMember(dest=>dest.PhotoEUrl,opt=>{
            opt.MapFrom(src=>src.PhotosE.FirstOrDefault(p=>p.Url!=null).Url);
            });
            CreateMap<PhotosEForReturnDto,PhotoE>();
             CreateMap<PhotosEForCreationDto,PhotoE>();
             CreateMap<NutritionFactForAddDto,NutritionFact>();
             CreateMap<NutritionFact,NutritionFactForDetailedDto>().ForMember(dest=>dest.PhotoNUrl,opt=>{
                opt.MapFrom(src=>src.PhotosE.FirstOrDefault(p=>p.IsMain).Url);
            });
            CreateMap<PhotoN,PhotosNForDetailedDto>();
            CreateMap<NutritionFact,NutritionFactsForListDto>().ForMember(dest=>dest.PhotoNUrl,opt=>{
                opt.MapFrom(src=>src.PhotosE.FirstOrDefault(p=>p.IsMain).Url);
            });
            CreateMap<NutritionFactForUpdateDto,NutritionFact>();
            CreateMap<PhotosNForReturnDto,PhotoN>();
            CreateMap<PhotosNForCreationDto,PhotoN>();
            

    }
    }
}
