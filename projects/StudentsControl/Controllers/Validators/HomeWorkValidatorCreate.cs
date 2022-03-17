namespace Controllers
{
    using FluentValidation;
    public class HomeWorkValidatorCreate : AbstractValidator<HomeWorkCreationDto>
    {
        public HomeWorkValidatorCreate()
        {
            RuleFor(x => x.Mark).InclusiveBetween(1, 5);
        }
    }
}