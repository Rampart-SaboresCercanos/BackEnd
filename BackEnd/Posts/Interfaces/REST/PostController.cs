using System.Net.Mime;
using BackEnd.Posts.Domain.Model.Aggregates;
using BackEnd.Posts.Domain.Model.Command;
using BackEnd.Posts.Domain.Model.Queries;
using BackEnd.Posts.Domain.Services;
using BackEnd.Posts.Interfaces.REST.Resource;
using BackEnd.Posts.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BackEnd.Posts.Interfaces.REST;

[ApiController]
[Route("/api/v1/posts")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Posts")]

public class PostController(
    IPostCommandService postCommandService,
    IPostQueryService postQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all posts",
        Description = "Get all posts",
        OperationId = "GetAllPosts")]
    [SwaggerResponse(201, "The posts were found", typeof(Post))]
    [SwaggerResponse(400, "The post were not found")]
    public async Task<ActionResult> GetAllPosts()
    {
        var query = new GetAllPostsQuery();
        var posts = await postQueryService.Handle(query);
        var postResources = posts.Select(PostResourceFromEntityAssembler.ToResourceFromEntity);
        if (postResources is null) return NotFound();
        return Ok(postResources);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a Post",
        Description = "Create a post by providing a Dish, a Date, a Stock number and a Price per Unit",
        OperationId = "CreatePost")]
    [SwaggerResponse(201, "The post was created", typeof(Post))]
    [SwaggerResponse(400, "The post was not created")]

    public async Task<ActionResult> CreatePost([FromBody] CreatePostResource resource)
    {
        var command = CreatePostCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await postCommandService.Handle(command);
        if (result is null)
        {
            return BadRequest("Post could not be created");
        }
        return CreatedAtAction(nameof(GetPostById), new { id=result.id },
            PostResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a post",
        Description = "Update a post with the specified ID",
        OperationId = "UpdatePOst")]
    [SwaggerResponse(200, "Post updated successfully")]
    [SwaggerResponse(404, "Post not found")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid data.");
        }
        
        var updatedCommand = command with { PostId = id };
        
        var result = await postCommandService.Handle(updatedCommand);

        if (result == null)
        {
            return NotFound($"Post with ID {id} not found.");
        }
        
        return Ok(new { Message = "Post updated successfully." });
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a post",
        Description = "Delete a post with the specified ID",
        OperationId = "DeletePost")]
    [SwaggerResponse(204, "Post was successfully deleted")]
    [SwaggerResponse(404, "POst not found")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        var result = await postCommandService.DeletePostAsync(id);
    
        if (!result)
        {
            return NotFound($"Post with ID {id} not found.");
        }

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetPostById(int id)
    {
        var getOrderById = new GetPostByIdQuery(id);
        var result = await postQueryService.Handle(getOrderById);
        if (result is null) return NotFound();
        var resources = PostResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resources);
    }
}
