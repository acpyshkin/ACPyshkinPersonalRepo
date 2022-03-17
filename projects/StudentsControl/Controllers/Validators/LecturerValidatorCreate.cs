namespace Controllers
{
    using FluentValidation;
    public class LecturerValidatorCreate : AbstractValidator<LecturerCreationDto>
    {
        public LecturerValidatorCreate()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}