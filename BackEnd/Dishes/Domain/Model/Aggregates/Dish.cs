using BackEnd.Dishes.Domain.Model.Commands;

namespace BackEnd.Dishes.Domain.Model.Aggregates;

public partial class Dish
{
    public int Id { get; }
    
    public  int ChefId { get; set; }
    public string NameOfDish  { get; set; }
    public bool Favorite { get; set; }
    
    public List<string> Ingredients { get; set; }
    public List<string> PreparationSteps { get; set; }
    
    protected Dish()
    {
        ChefId = 0;
        NameOfDish = string.Empty;
        Favorite = false;
        Ingredients = new List<string>();
        PreparationSteps = new List<string>();
    }
    
    public Dish(CreateDishCommand command)
    {
        ChefId = command.ChefId;
        NameOfDish = command.NameOfDish;
        Favorite = command.Favorite;
        Ingredients = command.Ingredients ?? new List<string>();
        PreparationSteps = command.PreparationSteps ?? new List<string>();
    }
    
    //2da manera
    /*
    // JSON para almacenar ingredientes
    public string ingredients { get; set; } = string.Empty;

    // JSON para almacenar pasos de preparación
    public string preparationSteps { get; set; } = string.Empty;

    // Propiedad para acceder a ingredientes como lista
    [NotMapped] // Esto es importante
    public List<string> Ingredients 
    { 
        get => string.IsNullOrEmpty(ingredients) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(ingredients) ?? new List<string>();
        set => ingredients = JsonSerializer.Serialize(value);
    }

    // Propiedad para acceder a pasos como lista
    [NotMapped] // Esto es importante
    public List<string> PreparationSteps 
    { 
        get => string.IsNullOrEmpty(preparationSteps) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(preparationSteps) ?? new List<string>();
        set => preparationSteps = JsonSerializer.Serialize(value);
    }
    
    protected Dish()
    {
        chefId = 0;
        nameOfDish = string.Empty;
        favorite = false;
    }

    
    public Dish(CreateDishCommand command)
    {
        chefId = command.ChefId;
        nameOfDish = command.NameOfDish;
        Ingredients = command.Ingredients;
        PreparationSteps = command.PreparationSteps;
        favorite = command.Favorite;
    }*/
}