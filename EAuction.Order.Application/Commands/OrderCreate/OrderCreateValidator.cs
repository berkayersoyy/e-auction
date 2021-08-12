using FluentValidation;

namespace EAuction.Order.Application.Commands.OrderCreate
{
    public class OrderCreateValidator:AbstractValidator<OrderCreateCommand>
    {
        public OrderCreateValidator()
        {
            RuleFor(x => x.SellerUserName).EmailAddress().NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
        }   
    }
}