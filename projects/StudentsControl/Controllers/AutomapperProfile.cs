namespace Controllers
{
    using System.Linq;
    using AutoMapper;
    using Domain;

    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<LectureCreationDto, LectureInputModel>();
            CreateMap<LectureModel, LectureDto>()
                .ForMember(dto => dto.StudentsThatAttend, config => config.MapFrom(model => model.StudentsThatAttend.Select(s => s.Id)))
                .ForMember(dto => dto.SubmitedHomeWorks, config => config.MapFrom(model => model.SubmitedHomeWorks.Select(s => s.Id)))
                .ForMember(dto => dto.Course, config => config.MapFrom(model => model.Course.Id));
            CreateMap<LectureDto, LectureInputModel>();

            CreateMap<CourseCreationDto, CourseInputModel>();
            CreateMap<CourseModel, CourseDto>()
                .ForMember(dto => dto.AppointedLecturesIdsList, config => config.MapFrom(model => model.AppointedLecturesList.Select(s => s.Id)))
                .ForMember(dto => dto.AppointedStudentsIdsList, config => config.MapFrom(model => model.AppointedStudentsList.Select(s => s.Id)))
                .ForMember(dto => dto.Lecturer, config => config.MapFrom(model => model.Lecturer.Id));
            CreateMap<CourseDto, CourseInputModel>();

            CreateMap<StudentCreationDto, StudentInputModel>().ReverseMap();
            CreateMap<StudentModel, StudentDto>()
                .ForMember(dto => dto.LectureAttandance, config => config.MapFrom(model => model.LectureAttandance.Select(s => s.Id)))
                .ForMember(dto => dto.AppointedHomeWorksList, config => config.MapFrom(model => model.AppointedHomeWorksList.Select(s => s.Id)))
                .ForMember(dto => dto.Course, config => config.MapFrom(model => model.Course.Id));
            CreateMap<StudentDto, StudentInputModel>();

            CreateMap<HomeWorkDto, HomeWorkInputModel>();
            CreateMap<HomeWorkCreationDto, HomeWorkInputModel>().ForMember(model => model.Id, config => config.Ignore());
            CreateMap<HomeWorkModel, HomeWorkDto>()
                .ForMember(dto => dto.RelevantLecture, config => config.MapFrom(model => model.RelevantLecture.Id))
                .ForMember(dto => dto.AssignedStudent, config => config.MapFrom(model => model.AssignedStudent.Id));

            CreateMap<LecturerDto, LecturerInputModel>().ReverseMap();
            CreateMap<LecturerCreationDto, LecturerInputModel>().ForMember(model => model.Id, config => config.Ignore());
            CreateMap<LecturerModel, LecturerDto>()
                .ForMember(dto => dto.Course, config => config.MapFrom(model => model.Course.Id));
        }
    }
}