using BackEnd.Orders.Domain.Model.Aggregates;
using BackEnd.Orders.Domain.Model.Command;
using BackEnd.Orders.Domain.Repositories;
using BackEnd.Orders.Domain.Services;
using BackEnd.Shared.Domain.Repositories;

namespace BackEnd.Orders.Application.Internal.CommandServices;

public class OrderCommandService(IOrderRepository orderRepository,
    IUnitOfWork unitOfWork ) : IOrderCommandService
{
    public async Task<Order?> Handle(CreateOrderCommand command)
    {
        //var existingProfile = await orderRepository.FindByEmailAsync(command.Email);
        //if (existingProfile != null)
            //throw new Exception("User profile with this email already exists.");

        var order = new Order(command);
        try
        {
            await orderRepository.AddAsync(order);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return order;
    }

    public async Task<Order?> Handle(UpdateOrderCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.UserId);
        if (order == null)
            throw new Exception("Order not found.");

        // Actualiza las propiedades del perfil según el comando
        if (command.customerId != null)
        {
            order.customerId = (int)command.customerId; // Actualiza la propiedad Photo
        }

        if (command.orderDate != null)
        {
            order.orderDate = (DateTime)command.orderDate;
        }

        if (command.deliveryDate != null)
        {
            order.deliveryDate = (DateTime)command.deliveryDate;
        }
        
        if (command.deliveryTime != null)
        {
            order.deliveryTime = command.deliveryTime;
        }

        if (command.paymentMethod!=null)
        {
            order.paymentMethod = command.paymentMethod;
        }

        if (command.status != null)
        {
            order.status = command.status;
        }

        try
        {
            await orderRepository.UpdateAsync(order);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return order; // Devuelve el perfil actualizado
    }

    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        var order = await orderRepository.FindByIdAsync(orderId);
        if (order == null)
        {
            return false; // No se encontró el perfil
        }

        try
        {
            await orderRepository.RemoveAsync(order); // Eliminar el perfil del repositorio
            await unitOfWork.CompleteAsync(); // Completar la unidad de trabajo
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return true; // Se eliminó correctamente
    }
}