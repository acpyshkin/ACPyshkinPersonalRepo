namespace BuisnessLogic
{
    using Domain;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServicesConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            // TODO scoped?
            services.AddTransient<ILectureService, LectureService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IHomeWorkService, HomeWorkSevice>();
            services.AddTransient<ILecturerService, LecturerService>();
            services.AddTransient<IAttendadanceSupervisingServise, AttendanceSupervisingService>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddTransient<IStudentPerformanceSupervision, StudentPerformanceSupervision>();
            services.AddTransient<ISmsSenderService, SmsSenderService>();
            services.AddTransient<IReportGenerator, ReportGenerator>();
        }
    }
}