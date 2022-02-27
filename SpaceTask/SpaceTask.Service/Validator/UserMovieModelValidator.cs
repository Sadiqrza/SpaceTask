
using FluentValidation;
using SpaceTask.Data.DataModels;

namespace SpaceTask.Service.Validator
{
    public class UserMovieModelValidator : AbstractValidator<UserMovie>
    {
        public UserMovieModelValidator()
        {
            RuleFor(m => m.UserId)
                .NotEmpty().WithMessage("{PropertyName} is empty!");

            RuleFor(m => m.MovieName)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .Length(1, 50).WithMessage("Length of {PropertyName} is invalid!");
        }
    }
}
