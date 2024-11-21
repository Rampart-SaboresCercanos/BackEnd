namespace BackEnd.Orders.Domain.Model.Command;

public record CreateOrderCommand(
    int customerId,
    DateTime orderDate,
    DateTime deliveryDate,
    string deliveryTime,
    string paymentMethod,
    string status
    );