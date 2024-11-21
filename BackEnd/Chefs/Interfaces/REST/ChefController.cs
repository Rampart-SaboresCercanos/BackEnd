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
[SwaggerTag("Create, read, update and delete chefs")]

public class ChefController(IChefCommandService chefCommandService,
    IChefQueryService chefQueryService) : ControllerBase
{
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get a chef by ID",  // Resumen de la operación
        Description = "Retrieve the chef details based on the specified chef ID",  // Descripción detallada
        OperationId = "GetChefById")]  // ID único de la operación
    [SwaggerResponse(200, "Chef retrieved successfully", typeof(ChefResource))]  // Respuesta cuando se encuentra el chef
    [SwaggerResponse(404, "Chef not found")]  // Respuesta cuando no se encuentra el chef
    public async Task<IActionResult> GetChefById(int id)
    {
        // Crear la consulta para obtener el chef por ID
        var getChefByIdQuery = new GetChefByIdQuery(id);

        // Ejecutar la consulta utilizando el servicio correspondiente
        var chef = await chefQueryService.Handle(getChefByIdQuery);

        // Si no se encuentra el chef, se retorna NotFound
        if (chef is null) return NotFound();

        // Convertir el resultado en un recurso para devolverlo en la respuesta
        var chefResource = ChefResourceFromEntityAssembler.ToResourceFromEntity(chef);

        // Retornar el chef encontrado con estado HTTP 200 (OK)
        return Ok(chefResource);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all chefs",  // Resumen de la operación
        Description = "Retrieve all the details of the chefs",  // Descripción detallada
        OperationId = "GetAllChefs"  // ID único de la operación
    )]    
    [SwaggerResponse(201, "All chefs", typeof(IEnumerable<ChefResource>))]
    [SwaggerResponse(400, "Bad request")]
    public async Task<IActionResult> GetAllChefs()
    {
        var getAllChefsQuery = new GetAllChefsQuery();
        var chefs = await chefQueryService.Handle(getAllChefsQuery);
        var chefResources = chefs.Select(ChefResourceFromEntityAssembler
            .ToResourceFromEntity);
        return Ok(chefResources);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a chef",
        Description = "Create a chef with the provided information",
        OperationId = "CreateChef")]
    [SwaggerResponse(201, "The chef was created", typeof(ChefResource))]
    [SwaggerResponse(400, "The chef was not created")]
    public async Task<IActionResult> CreateChef(CreateChefResource resource)
    {
        var createChefCommand = CreateChefCommandFromResourceAssembler.
            ToCommandFromResource(resource);
        var chef = await chefCommandService.Handle(createChefCommand);
        if (chef is null)
        {
            return BadRequest("Chef could not be created.");
        }
        var chefResource = ChefResourceFromEntityAssembler.ToResourceFromEntity(chef);
        return CreatedAtAction(nameof(GetChefById), new { id = chef.Id }, chefResource);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update a chef",
        Description = "Update a chef with the specified ID",
        OperationId = "UpdateChef")]
    [SwaggerResponse(200, "Chef updated successfully")]
    [SwaggerResponse(404, "Chef profile not found")]
    public async Task<IActionResult> UpdateChef(int id, UpdateChefResource resource)
    {
        var updateChefCommand = UpdateChefCommandFromResourceAssembler
            .ToCommandFromResource(id, resource); // Ahora pasamos el id y el resource
        var chef = await chefCommandService.Handle(updateChefCommand);
        if (chef == null)
        {
            return NotFound();
        }
        var chefResource = ChefResourceFromEntityAssembler.ToResourceFromEntity(chef);
        return Ok(new { Message = "Chef actualizado correctamente.", Chef = chefResource });
    }
    
    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Delete a chef",
        Description = "Delete a chef with the specified ID",
        OperationId = "DeleteChef")]
    [SwaggerResponse(204, "Chef was successfully deleted")]
    [SwaggerResponse(404, "Chef not found")]
    public async Task<IActionResult> DeleteChef(int id)
    {
        var result = await chefCommandService.DeleteChefCommand(id);  // Llamar al servicio para manejar el comando

        if (!result)
        {
            return NotFound($"Chef with ID {id} not found.");
        }

        return NoContent();  // Retornar NoContent cuando la eliminación sea exitosa
    }
}
