using System.Net.Mime;
using BackEnd.Chefs.Domain.Model.Command;
using BackEnd.Chefs.Domain.Model.Queries;
using BackEnd.Chefs.Domain.Services;
using BackEnd.Chefs.Interfaces.REST.Resource;
using BackEnd.Chefs.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BackEnd.Chefs.Interfaces.REST;

[ApiController]
[Route("/api/v1/chefs")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Chefs")]

public class ChefController(
    IChefCommandService chefCommandService,
    IChefQueryService chefQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a chef",
        Description = "Create a chef with the provided information",
        OperationId = "CreateChef")]
    [SwaggerResponse(201, "The chef was created", typeof(ChefResource))]
    [SwaggerResponse(400, "The chef was not created")]
    public async Task<ActionResult> CreateChef([FromBody] CreateChefResource resource)
    {
        var command = CreateChefCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await chefCommandService.Handle(command);
        if (result is null)
        {
            return BadRequest("Chef could not be created.");
        }
        return CreatedAtAction(nameof(GetChefById), new { id = result.Id },
            ChefResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a chef",
        Description = "Update a chef with the specified ID",
        OperationId = "UpdateChef")]
    [SwaggerResponse(200, "Chef updated successfully")]
    [SwaggerResponse(404, "Chef profile not found")]
    public async Task<IActionResult> UpdateChef(int id, [FromBody] UpdateChefCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid data.");
        }

        var updatedCommand = command with { ChefId = id };
        var result = await chefCommandService.Handle(updatedCommand);

        if (result == null)
        {
            return NotFound($"Chef with ID {id} not found.");
        }

        return Ok(new { Message = "Chef updated successfully." });
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a chef",
        Description = "Delete a chef with the specified ID",
        OperationId = "DeleteChef")]
    [SwaggerResponse(204, "Chef was successfully deleted")]
    [SwaggerResponse(404, "Chef not found")]
    public async Task<ActionResult> DeleteChef(int id)
    {
        var command = new DeleteChefCommand(id);  // Crear el comando de eliminación
        var result = await chefCommandService.Handle(command);  // Llamar al servicio para manejar el comando

        if (result == null)
        {
            return NotFound($"Chef with ID {id} not found.");
        }

        return NoContent();  // Retornar NoContent cuando la eliminación sea exitosa
    }

    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a chef by ID",  // Resumen de la operación
        Description = "Retrieve the chef details based on the specified chef ID",  // Descripción detallada
        OperationId = "GetChefById")]  // ID único de la operación
    [SwaggerResponse(200, "Chef retrieved successfully", typeof(ChefResource))]  // Respuesta cuando se encuentra el chef
    [SwaggerResponse(404, "Chef not found")]  // Respuesta cuando no se encuentra el chef
    public async Task<ActionResult> GetChefById(int id)
    {
        // Crear la consulta para obtener el chef por ID
        var getChefById = new GetChefByIdQuery(id);

        // Ejecutar la consulta utilizando el servicio correspondiente
        var result = await chefQueryService.Handle(getChefById);

        // Si no se encuentra el chef, se retorna NotFound
        if (result is null) return NotFound();

        // Convertir el resultado en un recurso para devolverlo en la respuesta
        var resources = ChefResourceFromEntityAssembler.ToResourceFromEntity(result);

        // Retornar el chef encontrado con estado HTTP 200 (OK)
        return Ok(resources);
    }
}
