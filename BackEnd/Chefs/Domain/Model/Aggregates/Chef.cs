using BackEnd.Chefs.Domain.Model.Command;

namespace BackEnd.Chefs.Domain.Model.Aggregates;

public partial class Chef
{
    public int Id { get; }
        
    public string Name { get; set; }
    public string Gender { get; set; }
    public double Rating { get; set; }
    public bool IsFavorite { get; set; }
        
    // Si quieres mantener listas de ingredientes u otros elementos, puedes agregarlos aquí
    // Por ejemplo, si un chef tiene una lista de platos favoritos, podrías agregarlo como una lista de Dish.
    // public List<Dish> Dishes { get; set; }
        
    // Constructor vacío (por si se usa para la deserialización o por conveniencia)
    protected Chef()
    {
        Name = string.Empty;
        Gender = string.Empty;
        Rating = 0.0;
        IsFavorite = false;
    }

    // Constructor con parámetros, que recibe un comando de creación
    public Chef(CreateChefCommand command)
    {
        Name = command.Name;
        Gender = command.Gender;
        Rating = command.Rating;
        IsFavorite = command.IsFavorite;
    }

    // Si quieres usar propiedades como listas serializadas en JSON
    /*
    // JSON para almacenar alguna información adicional
    public string dishes { get; set; } = string.Empty;

    [NotMapped]
    public List<Dish> Dishes
    {
        get => string.IsNullOrEmpty(dishes) ? new List<Dish>() : JsonSerializer.Deserialize<List<Dish>>(dishes) ?? new List<Dish>();
        set => dishes = JsonSerializer.Serialize(value);
    }
    */

    // Método para actualizar un Chef, por ejemplo, cambiando su estado de favorito
    public void ToggleFavorite()
    {
        IsFavorite = !IsFavorite;
    }
}