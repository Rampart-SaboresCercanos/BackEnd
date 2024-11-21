using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace BackEnd.Chefs.Domain.Model.Aggregates;

public partial class Chef : IEntityWithCreatedUpdatedDate
{
    // Propiedad que almacena la fecha de creación del chef
    [Column("CreatedAt")]
    public DateTimeOffset? CreatedDate { get; set; }

    // Propiedad que almacena la fecha de última actualización del chef
    [Column("UpdatedAt")]
    public DateTimeOffset? UpdatedDate { get; set; }
}