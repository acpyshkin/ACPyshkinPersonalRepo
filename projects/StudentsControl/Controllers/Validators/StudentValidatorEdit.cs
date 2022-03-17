namespace Controllers
{
    using FluentValidation;
    public class StudentValidatorEdit : AbstractValidator<StudentDto>
    {
        public StudentValidatorEdit()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.PhoneNumber).InclusiveBetween(1, int.MaxValue);
        }
    }
}