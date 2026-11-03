using BogdaoTech.Domain.Modules;
using BogdaoTech.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BogdaoTech.Infrastructure.Configuration
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        //Реализуем вот такой метод этого интерфейса
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            //название таблички
            builder.ToTable("issues");

            //что является ключом
            builder.HasKey(i => i.Id);

            //у модели есть свойство и как оно будет конвертироваться в БД
            builder.Property(i => i.Id)
                .HasConversion(
                     id => id.Value,
                     value => IssueId.Create(value)
                );

            builder.Property(i => i.LessonId)
                .IsRequired(false);

            //настройка свойства заголовок у модели модуля
            builder.Property(i => i.Title)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            //настройка свойства описание у модели модуля
            builder.Property(i => i.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            //У задачи есть связь родительская, а у родительской есть много дочерних задач
            //и они необязательные и при удалении не затрагиваем дочерние подзадачи
            builder.HasOne(i => i.ParentIssue)
                .WithMany(p => p.SubIssues)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            //У задачи есть issueDetails, переводим их в Json формат и достаем из деталей файлы
            //конфигурируем Json-файлы
            builder.OwnsOne(i => i.Details, ib =>
            {
                ib.ToJson();

                ib.OwnsMany(d => d.Files, fb =>
                {
                    fb.Property(f => f.PathToStorage)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                });
            });
        }
    }
}
