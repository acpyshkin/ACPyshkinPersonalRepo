namespace Controllers
{
    using FluentValidation;
    public class StudentValidatorCreate : AbstractValidator<StudentCreationDto>
    {
        public StudentValidatorCreate()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.PhoneNumber).InclusiveBetween(1, int.MaxValue);
        }
    }
}