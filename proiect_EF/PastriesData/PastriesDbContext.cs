using Microsoft.EntityFrameworkCore;
using PastriesCommon.Entities;
using PastriesCommon.Entities.InterfacesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PastriesData
{
    public class PastriesDbContext:DbContext
    {
        public PastriesDbContext(DbContextOptions<PastriesDbContext> options) : base(options)
        {

        }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PastriesFactory> PastriesFactories { get; set; }
        public DbSet<User> Users { get; set; }
        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.ApplyConfiguration(new IngredientConfiguration());
         }*/
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SoftDelete();

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IngredientConfiguration());

            // Set delete behavior for entities decorated with IRestrictedCascadeDelete
            var restrictedDeleteRelationships = modelBuilder.Model.GetEntityTypes()
                .Where(x => typeof(IRestrictedCascadeDelete).IsAssignableFrom(x.ClrType))
                .SelectMany(x => x.GetForeignKeys());
            foreach (var relationship in restrictedDeleteRelationships)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            var deletedRelationships = modelBuilder.Model.GetEntityTypes()
                .Where(x => typeof(ISoftDelete).IsAssignableFrom(x.ClrType));
            foreach (var relationship in deletedRelationships)
            {
                modelBuilder.AddIsDeletedColumnFilter(relationship.ClrType);
                modelBuilder.SetSoftDeleteFilter(relationship.ClrType);
            }
        }

        

        private void SoftDelete()
        {//la soft delete sterge automat din tabelul m-n
            var entities = ChangeTracker.Entries()
                .Where(item => item.Entity is ISoftDelete && item.State == EntityState.Deleted);

            foreach (var entity in entities)
            {
                entity.State = EntityState.Modified;
                entity.CurrentValues[BulkConfigurationExtensions.IsDeletedColumnName] = true;
            }
        }
    }
}
