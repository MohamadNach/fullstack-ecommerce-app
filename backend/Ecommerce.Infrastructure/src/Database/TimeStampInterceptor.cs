// using Microsoft.EntityFrameworkCore.Diagnostics;
// using Microsoft.EntityFrameworkCore;
// using Ecommerce.Domain.src.Shared;

// namespace Ecommerce.Infrastructure.src.Database
// {
//     public class TimeStampInterceptor : SaveChangesInterceptor
//     {
//         public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
//         {
//             var entries = eventData.Context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

//             foreach (var entry in entries)
//             {
//                 if (entry.Entity is BaseEntity entity)
//                 {
//                     if (entry.State == EntityState.Added)
//                     {
//                         entity.CreatedAt = DateTime.UtcNow;
//                     }

//                     entity.UpdatedAt = DateTime.UtcNow;
//                 }
//             }

//             return base.SavingChanges(eventData, result);
//         }

//     }
// }