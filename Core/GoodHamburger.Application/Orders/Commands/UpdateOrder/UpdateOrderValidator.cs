using FluentValidation;

namespace GoodHamburger.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
            .WithMessage("ID do pedido inválido.");

        RuleFor(x => x.ProductIds)
            .NotEmpty()
            .WithMessage("O pedido deve ter ao menos um item.");

        RuleFor(x => x.ProductIds)
            .Must(ids => ids.Count <= 3)
            .WithMessage("O pedido não pode ter mais de 3 itens.");
    }
}