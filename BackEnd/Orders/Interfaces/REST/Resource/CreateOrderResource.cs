namespace BackEnd.Orders.Interfaces.REST.Resource;



public record CreateOrderResource(
    int customerId,
    DateTime orderDate,
    DateTime deliveryDate,
    string deliveryTime,
    string paymentMethod,
    string? status  // Cambiado a string? para hacerlo opcional
);