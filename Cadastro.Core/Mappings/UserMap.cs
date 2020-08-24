using Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Core.Mappings
{
    public static partial class Mappings
    {
        public static void UserMap(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).HasColumnType("varchar(250)").IsRequired();
                entity.Property(e => e.Name).HasColumnType("varchar(250)").IsRequired();
                entity.Property(e => e.Password).HasColumnType("varchar(60)").IsRequired();
                entity.Property(e => e.Created).HasDefaultValueSql("GETDATE()").IsRequired();
                entity.Property(e => e.Last_login).HasDefaultValueSql("GETDATE()").IsRequired();

                entity.HasMany(x => x.Phones).WithOne().HasForeignKey(y => y.UserId);
            });

        }
    }
}
