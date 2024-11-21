using System.Text.Json;
using BackEnd.Posts.Domain.Model.Command;

namespace BackEnd.Posts.Domain.Model.Aggregates;

public class Post
{
    public int id { get; set; }
    public int dishId { get; set; }
    public DateTime publishDate { get; set; }
    public int stock { get; set; }
    
    public float pricePerUnit { get; set; }
    
    protected Post() { }

    public Post(CreatePostCommand command)
    {
        dishId = command.dishId;
        publishDate = command.publishDate;
        stock = command.stock;
        pricePerUnit = command.pricePerUnit;
    }
}