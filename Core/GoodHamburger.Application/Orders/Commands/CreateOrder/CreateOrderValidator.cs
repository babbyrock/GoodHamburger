using FluentValidation;

namespace GoodHamburger.Application.Orders.Commands.CreateOrder;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.ProductIds)
            .NotEmpty()
            .WithMessage("O pedido deve ter ao menos um item.");

        RuleFor(x => x.ProductIds)
            .Must(ids => ids.Count <= 3)
            .WithMessage("O pedido não pode ter mais de 3 itens.");
    }
}