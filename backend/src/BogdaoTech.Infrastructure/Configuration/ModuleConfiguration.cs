using BogdaoTech.Domain.Modules;
using BogdaoTech.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

//Настраиваем то, как в БД будет выглядеть наша модель Модуль
//т.е. ее таблицы
namespace BogdaoTech.Infrastructure.Configuration
{
    //надо наследоваться от интерфейса, который и позволяет
    //настроить эту конфигурацию
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        //Реализуем вот такой метод этого интерфейса
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            //название таблички
            builder.ToTable("modules");

            //что является ключом
            builder.HasKey(m => m.Id);

            //у модели есть свойство и как оно будет конвертироваться в БД
            builder.Property(m => m.Id)
                .HasConversion(
                     id => id.Value,
                     value => ModuleId.Create(value)
                );

            builder.ComplexProperty(m => m.Title, tb =>
            {
                tb.Property(t => t.Value)
                    .IsRequired()
                    .HasMaxLength(Title.MAX_LENGTH)
                    .HasColumnName("title");
            });

            builder.ComplexProperty(m => m.Description, db =>
            {
                db.Property(d => d.Value)
                    .IsRequired()
                    .HasMaxLength(Description.MAX_LENGTH)
                    .HasColumnName("description");
            });

            //У модуля есть много задач, а у каждого задания есть один модуль
            builder.HasMany(m => m.Issues)
                .WithOne()
                .HasForeignKey("module_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_deleted");
        }
    }
}
