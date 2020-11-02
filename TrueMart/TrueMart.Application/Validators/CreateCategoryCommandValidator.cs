using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TrueMart.Application.Common.Consts;
using TrueMart.Application.CQRS.Category.Command;
using TrueMart.Application.DatabaseServices;

namespace TrueMart.Application.Validators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryCommandValidator(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            RuleFor(t => t.Name).NotNull().WithMessage(ValidateMessage.NameCanNotBeNull);
            RuleFor(t => t.Name).MustAsync(async (name, cancellationToken) => (await _categoryService.IsUniqueName(name, cancellationToken)))
                .WithMessage(ValidateMessage.NameAlreadyExists);
        }
    }
}
