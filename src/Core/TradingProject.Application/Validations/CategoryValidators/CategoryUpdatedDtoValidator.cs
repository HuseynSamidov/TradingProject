using FluentValidation;
using TradingProject.Application.DTOs.CategoryDtos;

public class CategoryUpdatedDtoValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdatedDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name can not be null")
            .MinimumLength(3).WithMessage("Name should be minumum 3 character");
    }
}
