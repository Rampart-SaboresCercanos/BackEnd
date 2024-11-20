using System.Net.Mime;
using Backend.Dishes.Domain.Model.Commands;
using Backend.Dishes.Domain.Model.Queries;
using Backend.Dishes.Domain.services;
using Backend.Dishes.Interfaces.REST.Resources;
using Backend.Dishes.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Dishes.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete dishes")]
public class DishController(IDishQueryService dishQueryService,
    IDishCommandService dishCommandService): ControllerBase
{
    [HttpGet("{dishId:int}")]
    public async Task<IActionResult> GetDishById(int dishId)
    {
        var getDishByIdQuery = new GetDishByIdQuery(dishId);
        var dish = await dishQueryService.Handle(getDishByIdQuery);
        if (dish == null)
        {
            return NotFound();
        }
        var dishResource = DishResourceFromEntityAssembler.ToResourceFromEntity(dish);
        return Ok(dishResource);
    }
    
    [HttpGet]
    [SwaggerOperation("Get all dishes")]
    [SwaggerResponse(201, "All dishes", typeof(IEnumerable<DishResource>))]
    [SwaggerResponse(400, "Bad request")]
    public async Task<IActionResult> GetAllDishes()
    {
        var getAllDishesQuery = new GetAllDishesQuery();
        var dishes = await dishQueryService.Handle(getAllDishesQuery);
        var dishResources = dishes.Select(DishResourceFromEntityAssembler
            .ToResourceFromEntity);
        return Ok(dishResources);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDish(CreateDishResource resource)
    {
        var createDishCommand = CreateDishCommandFromResourceAssembler
            .ToCommandFromResource(resource);
        var dish = await dishCommandService.Handle(createDishCommand);
        if (dish == null)
        {
            return BadRequest();
        }
        var dishResource = DishResourceFromEntityAssembler.ToResourceFromEntity(dish);
        return CreatedAtAction(nameof(GetDishById), new { dishId = dish.Id }, dishResource);
    }
    
    [HttpPut("{dishId:int}")]
    public async Task<IActionResult> UpdateDish(int dishId, UpdateDishResource resource)
    {
        if (dishId != resource.Id)
        {
            return BadRequest("Dish ID mismatch");
        }

        var updateDishCommand = UpdateDishCommandFromResourceAssembler
            .ToCommandFromResource(resource);
        var dish = await dishCommandService.Handle(updateDishCommand);
        if (dish == null)
        {
            return NotFound();
        }
        var dishResource = DishResourceFromEntityAssembler.ToResourceFromEntity(dish);
        return Ok(dishResource);
    }
    
    [HttpDelete("{dishId:int}")]
    public async Task<IActionResult> DeleteDish(int dishId)
    {
        var deleteDishCommand = new DeleteDishCommand(dishId);
        var result = await dishCommandService.DeleteDishCommand(dishId);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}