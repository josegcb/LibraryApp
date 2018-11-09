using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace LibraryAp.EntityFramework.Repositories
{
    public abstract class LibraryApRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<LibraryApDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected LibraryApRepositoryBase(IDbContextProvider<LibraryApDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class LibraryApRepositoryBase<TEntity> : LibraryApRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected LibraryApRepositoryBase(IDbContextProvider<LibraryApDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
