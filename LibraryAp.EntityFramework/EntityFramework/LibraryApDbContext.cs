using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using LibraryAp.Authorization.Roles;
using LibraryAp.Authorization.Users;
using LibraryAp.Models;
using LibraryAp.MultiTenancy;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System;
using System.Data.Entity.Core;
using Lib;
using EntityFramework.DynamicFilters;
using Abp.UI;

namespace LibraryAp.EntityFramework {
    public class LibraryApDbContext : AbpZeroDbContext<Tenant, Role, User> {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */

        public DbSet<Agencia> Agencias { get; set; }
        public DbSet<Caja> Cajas { get; set; }
        public LibraryApDbContext()
            : base("Default") {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in LibraryApDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of LibraryApDbContext since ABP automatically handles it.
         */
        public LibraryApDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString) {

        }

        //This constructor is used in tests
        public LibraryApDbContext(DbConnection existingConnection)
         : base(existingConnection, false) {

        }

        public LibraryApDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection) {

        }

        public override Task<int> SaveChangesAsync() {
            try {
                Task<int> vResult = base.SaveChangesAsync();
                return vResult;
            } catch (DbUpdateConcurrencyException e) {
                throw new UserFriendlyException("El registro fue modificado o eliminado por otro usuario");
            } catch (OptimisticConcurrencyException e) {
                throw new UserFriendlyException("El registro fue modificado o eliminado por otro usuario");
            } catch (Exception e) {
                throw e;
            }

        }
        public override int SaveChanges() {
            try {
                int vResult = base.SaveChanges();

                return vResult;
            } catch (DbUpdateConcurrencyException e) {
                throw new UserFriendlyException(485,"El registro fue modificado o eliminado por otro usuario");
            } catch (OptimisticConcurrencyException e) {
                throw new UserFriendlyException("El registro fue modificado o eliminado por otro usuario");
            } catch (Exception e) {
                throw e;
            }            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            ModelBuilderHelper.DecimalPrecisionConfiguration(modelBuilder);
            ModelBuilderHelper.RowVersionConfiguration(modelBuilder);
            ModelBuilderHelper.StringConfiguration(modelBuilder);
            modelBuilder.Filter(DataFilters.MustHaveAgency , (Lib.IMustHaveAgency  entity, int AgenciaId) 
                => entity.AgenciaId  == AgenciaId, 0);
        }
    }
}
