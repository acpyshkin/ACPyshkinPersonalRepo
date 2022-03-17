namespace Domain
{
    using AutoMapper;

    public class AutomapperProfileDomain : Profile
    {
        public AutomapperProfileDomain()
        {
            CreateMap<LectureInputModel, LectureInputEntity>();
            CreateMap<LectureEntity, LectureModel>().ReverseMap();

            CreateMap<CourseInputModel, CourseInputEntity>();
            CreateMap<CourseEntity, CourseModel>().ReverseMap();

            CreateMap<StudentInputModel, StudentInputEntity>();
            CreateMap<StudentModel, StudentEntity>().ReverseMap();

            CreateMap<HomeWorkInputModel, HomeWorkInputEntity>();
            CreateMap<HomeWorkModel, HomeWorkEntity>().ReverseMap();

            CreateMap<LecturerInputModel, LecturerInputEntity>();
            CreateMap<LecturerModel, LecturerEntity>().ReverseMap();
        }
    }
}