namespace Magisterka.Migrations
{
    using System.Data.Entity.Migrations;

    using Magisterka.Database;

    public class Configuration : DbMigrationsConfiguration<Entities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Magiterka.Entities";
        }
    }
}