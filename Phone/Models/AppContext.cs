using System.Data.Entity;

namespace Phone.Models
{
    public class AppContext : DbContext
    {
        public AppContext() : base("name=AppContext")
        {
        }

        public DbSet<CellPhone> CellPhones { get; set; }
    
    }
}
