using Cadastro.Core.Mappings;
using Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Cadastro.Core.Contexts
{
    public class PrincipalContext : DbContext
    {
        public PrincipalContext(DbContextOptions<PrincipalContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Phone> Phone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Maps();

            base.OnModelCreating(modelBuilder);

        }
    }
}
