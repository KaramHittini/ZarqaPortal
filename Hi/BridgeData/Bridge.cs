using Microsoft.EntityFrameworkCore;
using Hi.Models;

namespace Hi.BridgeData
{
    public class Bridge : DbContext
    {
        public Bridge(DbContextOptions<Bridge> options) : base(options)
        {

        }
        public DbSet<Class> Show { get; set; }
    }
}
