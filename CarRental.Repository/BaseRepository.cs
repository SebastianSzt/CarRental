using CarRental.Model;

namespace CarRental.Repository
{
    public abstract class BaseRepository
    {
        protected AppDbContext DbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
