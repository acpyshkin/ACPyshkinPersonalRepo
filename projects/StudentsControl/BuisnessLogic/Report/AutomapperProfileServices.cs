namespace BuisnessLogic
{
    using AutoMapper;
    using Domain;

    public class AutomapperProfileServices : Profile
    {
        public AutomapperProfileServices()
        {
            CreateMap<LectureModel, LectureForReport>()
                .ForMember(report => report.CourseID, config => config.MapFrom(entity => entity.Course.Id))
                .ForMember(report => report.CourseName, config => config.MapFrom(entity => entity.Course.Name))
                .ForMember(report => report.StudentsThatAttentLecture, config => config.Ignore());
            CreateMap<StudentModel, StudentForReport>()
                .ForMember(report => report.CourseID, config => config.MapFrom(entity => entity.Course.Id))
                .ForMember(report => report.CourseName, config => config.MapFrom(entity => entity.Course.Name))
                .ForMember(report => report.LecturesThatStudentAttend, config => config.Ignore());
        }
    }
}