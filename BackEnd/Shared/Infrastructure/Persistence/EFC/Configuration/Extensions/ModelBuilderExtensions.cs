
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void UseSnakeCaseNamingConvention(this ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName().ToSnakeCase());
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToSnakeCase());
            }
            foreach (var key in entity.GetKeys())
            {
                key.SetName(key.GetName().ToSnakeCase());
            }
            foreach (var key in entity.GetForeignKeys())
            {
                key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
            }
            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(index.GetDatabaseName().ToSnakeCase());
            }
        }
    }
}