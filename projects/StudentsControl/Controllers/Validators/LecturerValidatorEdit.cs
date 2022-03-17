namespace Controllers
{
    using FluentValidation;
    public class LecturerValidatorEdit : AbstractValidator<LecturerDto>
    {
        public LecturerValidatorEdit()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}