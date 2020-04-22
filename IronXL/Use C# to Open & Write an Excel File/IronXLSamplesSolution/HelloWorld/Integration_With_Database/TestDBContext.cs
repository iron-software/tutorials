using System.Data.Entity;

namespace HelloWorld.Integration_With_Database
{
    public class TestDBContext : DbContext
    {
        public TestDBContext()
           : base("name=TestDBContext")
        {
        }     

        public virtual DbSet<Country> Countries { get; set; }
    }
}
