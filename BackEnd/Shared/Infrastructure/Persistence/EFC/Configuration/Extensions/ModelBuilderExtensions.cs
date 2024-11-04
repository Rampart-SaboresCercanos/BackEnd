﻿using System.Collections.Immutable;
using System.Reflection.Metadata;

using Microsoft.EntityFrameworkCore;

namespace catch_up_platform_firtness.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;


public static class ModelBuilderExtensions
{
    public static void UseSnakeCaseNamingConvention(this ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            var tableName= entity.GetTableName();
            if (!string.IsNullOrEmpty(tableName)) entity.SetTableName(tableName.ToPlural().ToSnakeCase());

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToSnakeCase());
            }

            foreach (var key in entity.GetKeys())
            {
                var keyName = key.GetName();
                if(!string.IsNullOrEmpty(keyName)) key.SetName(keyName.ToSnakeCase());
            }

            foreach (var foreignKey in entity.GetForeignKeys())
            {
                var foreignKeyName=foreignKey.GetConstraintName();
                if(!string.IsNullOrEmpty(foreignKeyName)) foreignKey.SetConstraintName(foreignKeyName.ToSnakeCase());
            }
            
            foreach (var index in entity.GetIndexes())
            {
                var indexName=index.GetDatabaseName();
                if(!string.IsNullOrEmpty(indexName)) index.SetDatabaseName(indexName.ToSnakeCase());
            }
        }
    }
}