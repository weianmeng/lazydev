﻿using FluentValidation;

namespace SampleWeb.Service.Models
{
    public class AddStudentRequestValidator: AbstractValidator<AddStudentRequest>
    {
        public AddStudentRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("名称不能为空");
            RuleFor(x => x.Age).GreaterThan(10).WithMessage("年龄不能小于10");
        }
    }
}
