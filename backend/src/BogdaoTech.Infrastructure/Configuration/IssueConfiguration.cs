using BogdaoTech.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.ComplexProperty(i => i.LessonId, lb =>
            {
                lb.Property(l => l.Value)
                .IsRequired(false)
                .HasColumnName("lesson_id");
            });

            builder.ComplexProperty(i => i.Title, tb =>
            {
                tb.Property(t => t.Value)
                     .IsRequired()
                     .HasMaxLength(Title.MAX_LENGTH)
                     .HasColumnName("title");
            });

            builder.ComplexProperty(i => i.Description, db =>
            {
                db.Property(d => d.Value)
                     .IsRequired()
                     .HasMaxLength(Description.MAX_LENGTH)
                     .HasColumnName("description");
            });

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
                ib.ToJson("files");

                ib.OwnsMany(d => d.Files, fb =>
                {
                    fb.Property(f => f.PathToStorage)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                });
            });

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_deleted");
        }
    }
}
