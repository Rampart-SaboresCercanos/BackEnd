using BackEnd.Orders.Domain.Model.Aggregates;

namespace BackEnd.Orders.Interfaces.REST.Transform.Transform;

public static class OrderResourceFromEntityAssembler
{
    //Crear un objeto para la tabla ->Tabla ORDERS
    //ES lo que se muestra en el response
    public static OrderResource ToResourceFromEntity(Order entity)
    {
        // Asegúrate de que entity.Preferences sea de tipo List<UserPreference>
       

        return new OrderResource(
            entity.Id,
            entity.customerId,
            entity.orderDate,
            entity.deliveryDate,
            entity.deliveryTime,
            entity.paymentMethod,
            entity.status
            
        );
    }
}