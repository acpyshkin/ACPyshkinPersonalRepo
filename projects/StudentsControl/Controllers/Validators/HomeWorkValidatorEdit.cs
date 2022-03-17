namespace Controllers
{
    using FluentValidation;
    public class HomeWorkValidatorEdit : AbstractValidator<HomeWorkDto>
    {
        public HomeWorkValidatorEdit()
        {
            RuleFor(x => x.Mark).InclusiveBetween(1, 5);
        }
    }
}