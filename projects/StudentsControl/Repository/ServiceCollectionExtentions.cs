namespace Repository
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtentions
    {
        public static void AddRepositories(this IServiceCollection services, IConfiguration Configuration)
        {
            // TODO scoped?
            services.AddTransient<ILectureRepository, LectureRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IHomeWorkRepository, HomeWorkRepository>();
            services.AddTransient<ILecturerRepository, LecturerRepository>();

            string con = Configuration.GetConnectionString("DefaultConnection");

            //string con = "Data Source=(localdb)\\MSSQLLocalDB;Database = m10DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(con));
        }
    }
}