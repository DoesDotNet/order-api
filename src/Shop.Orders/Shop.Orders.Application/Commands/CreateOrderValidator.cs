using FluentValidation;

namespace Shop.Orders.Application.Commands
{
    public class CreateOrderValidator : AbstractValidator<CreateOrder>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id cannot be an empty Guid mofo");

            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("CustomerId cannot be an empty Guid mofo");
        }
    }
}
