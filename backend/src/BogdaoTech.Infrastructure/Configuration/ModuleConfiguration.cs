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

            //настройка свойства заголовок у модели модуля
            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            //настройка свойства описание у модели модуля
            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            //У модуля есть много задач, а у каждого задания есть один модуль
            builder.HasMany(m => m.Issues)
                .WithOne()
                .HasForeignKey("module_id");
        }
    }
}
