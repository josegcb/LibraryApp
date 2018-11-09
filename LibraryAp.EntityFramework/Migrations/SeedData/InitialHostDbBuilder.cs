using LibraryAp.EntityFramework;
using EntityFramework.DynamicFilters;

namespace LibraryAp.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly LibraryApDbContext _context;

        public InitialHostDbBuilder(LibraryApDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
