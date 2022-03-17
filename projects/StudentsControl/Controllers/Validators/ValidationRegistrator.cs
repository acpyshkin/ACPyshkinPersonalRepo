namespace Controllers
{
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    public static class ValidationRegistrator
    {
        public static void AddValidations(this IServiceCollection services)
        {
            services.AddTransient<IValidator<StudentCreationDto>, StudentValidatorCreate>();
            services.AddTransient<IValidator<StudentDto>, StudentValidatorEdit>();
            services.AddTransient<IValidator<LecturerCreationDto>, LecturerValidatorCreate>();
            services.AddTransient<IValidator<LecturerDto>, LecturerValidatorEdit>();
            services.AddTransient<IValidator<HomeWorkCreationDto>, HomeWorkValidatorCreate>();
            services.AddTransient<IValidator<HomeWorkDto>, HomeWorkValidatorEdit>();
        }
    }
}