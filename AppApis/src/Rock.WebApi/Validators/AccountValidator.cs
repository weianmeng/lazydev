using FluentValidation;
using Rock.Core.AccountApp.Dto;

namespace Rock.WebApi.Validators
{
    public class AccountValidator:AbstractValidator<AccountInput>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Mobile)
                .NotEmpty().WithMessage("手机号不能为空")
                .MaximumLength(15).WithMessage("手机号最大长度15");
        }
    }
}
