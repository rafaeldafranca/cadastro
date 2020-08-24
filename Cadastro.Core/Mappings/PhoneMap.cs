using Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Core.Mappings
{
    public static partial class Mappings
    {
        public static void PhoneMap(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>(entity =>
            {
                entity.ToTable("Phones");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.Ddd).HasColumnType("char(2)").IsRequired();
                entity.Property(e => e.Number).HasColumnType("varchar(20)").IsRequired();

            });
        }
       
    }
}
