using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BogdaoTech.Domain.Shared;

namespace BogdaoTech.Infrastructure.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override async ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData, 
            int result, 
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return await base.SavedChangesAsync(eventData, result, cancellationToken);
            }
            
            var entries = eventData.Context.ChangeTracker
                .Entries<ISoftDeletable>()
                .Where(e => e.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                entry.State = EntityState.Modified;
                entry.Entity.Delete();
            }

            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
