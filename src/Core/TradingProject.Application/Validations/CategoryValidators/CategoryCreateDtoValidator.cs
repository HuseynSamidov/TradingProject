using FluentValidation;
using TradingProject.Application.DTOs.CategoryDtos;

namespace TradingProject.Application.Validations.CategoryValidators
{
    public class CategoryCreatedDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreatedDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name can not be null")
                .MinimumLength(3).WithMessage("Name should be minumum 3 character")
                .MaximumLength(50).WithMessage("Name can only contains 50 characters");
        }
    }

}
