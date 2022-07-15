using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Todo.BLL.Models;

namespace Todo.BLL.Validation
{
    public class TodoItemValidator : AbstractValidator<TodoItemDTO>
    {
        public TodoItemValidator()
        {
            RuleFor(c => c.Name)
                   .NotEmpty().WithMessage("Наименование не может быть пустым");
        }
    }
}
