namespace BackEnd.Orders.Interfaces.REST.Transform;

public record OrderResource(
    int Id,
    int customerId,
    DateTime orderDate,
    DateTime deliveryDate,
    string deliveryTime,
    string paymentMethod,
    string? status  // Cambiado a string? para hacerlo opcional
    
    );