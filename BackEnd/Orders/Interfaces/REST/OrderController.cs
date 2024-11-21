using System.Net.Mime;
using BackEnd.Orders.Domain.Model.Command;
using BackEnd.Orders.Domain.Model.Queries;
using BackEnd.Orders.Domain.Services;
using BackEnd.Orders.Interfaces.REST.Resource;
using BackEnd.Orders.Interfaces.REST.Transform.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BackEnd.Orders.Interfaces.REST.Transform;

[ApiController]
[Route("/api/v1/orders")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Orders")]

public class OrderController(
    IOrderCommandService orderCommandService,
    IOrderQueryService orderQueryService) : ControllerBase
{

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a order",
        Description = "Create a order with the provided user information",
        OperationId = "CreateOrder")]
    [SwaggerResponse(201, "The order was created", typeof(OrderResource))]
    [SwaggerResponse(400, "The order was not created")]
    
    public async Task<ActionResult> CreateOrder([FromBody] CreateOrderResource resource)
    {
        var command = CreateOrderCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await orderCommandService.Handle(command);
        if (result is null)
        {
            return BadRequest("Order could not be created.");
        }
        return CreatedAtAction(nameof(GetOrderById), new { id = result.Id },
            OrderResourceFromEntityAssembler.ToResourceFromEntity(result));
        
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a order",
        Description = "Update a order with the specified ID",
        OperationId = "UpdateOrder")]
    [SwaggerResponse(200, "Order updated successfully")]
    [SwaggerResponse(404, "Order profile not found")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid data.");
        }

        // Utiliza el id de la URL para el perfil a actualizar
        var updatedCommand = command with { UserId = id };

        // Llama al servicio de comando para actualizar el perfil
        var result = await orderCommandService.Handle(updatedCommand);

        if (result == null)
        {
            return NotFound($"Order with ID {id} not found.");
        }

        // Retorna un mensaje simple indicando que la actualización fue exitosa
        return Ok(new { Message = "Order updated successfully." });
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a order",
        Description = "Delete a order with the specified ID",
        OperationId = "DeleteOrder")]
    [SwaggerResponse(204, "Order was successfully deleted")]
    [SwaggerResponse(404, "Order not found")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        // Llama al servicio de comandos para eliminar el perfil
        var result = await orderCommandService.DeleteOrderAsync(id);
    
        if (!result)
        {
            return NotFound($"Order with ID {id} not found."); // Si no se encontró, retorna 404
        }

        return NoContent(); // Retorna 204 No Content si se elimina correctamente
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetOrderById(int id)
    {
        var getOrderById = new GetOrderByIdQuery(id);
        var result = await orderQueryService.Handle(getOrderById);
        if (result is null) return NotFound();
        var resources = OrderResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resources);
    }
    
    //GetAllOrdersQuery
    [HttpGet()]
    [SwaggerOperation("Get all orders")]
    [SwaggerResponse(200, "All orders retrieved successfully", typeof(IEnumerable<OrderResource>))]
    [SwaggerResponse(400, "Bad request")]
    public async Task<ActionResult> GetAllOrders()
    {
        var result = await orderQueryService.Handle(new GetAllOrdersQuery());
        var orderResources = result.Select(OrderResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(orderResources);
    }
}