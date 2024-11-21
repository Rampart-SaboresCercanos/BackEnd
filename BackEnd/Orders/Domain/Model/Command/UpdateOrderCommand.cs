namespace BackEnd.Orders.Domain.Model.Command;

public record UpdateOrderCommand(
    int? customerId=null,
    DateTime? orderDate=null,
    DateTime? deliveryDate=null,
    string? deliveryTime=null,
    string? paymentMethod=null,
    string? status=null
    )
{
    // Solo se asignará internamente, no se mostrará en Swagger
    internal int UserId { get; init; }
};