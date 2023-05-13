using Microsoft.EntityFrameworkCore;
using ProFilePOC2.Domain.Entities;
using ProFilePOC2.Domain.Entities.Physical;

namespace ProFilePOC2.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    // Common entities
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<Department> Departments { get; set; }
    DbSet<UserGroup> UserGroups { get; set; }

    // Physical concerned entities
    DbSet<Document> Documents { get; set; }
    DbSet<Acquisition> Acquisitions { get; set; }
    DbSet<Room> Rooms { get; set; }
    DbSet<Locker> Lockers { get; set; }
    DbSet<PhysicalFolder> PhysicalFolders { get; set; }

    // Digital concerned entities
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
