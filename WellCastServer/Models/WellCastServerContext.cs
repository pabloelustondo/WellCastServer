using System.Data.Entity;

namespace WellCastServer.Models
{
    public class WellCastServerContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<WellCastServer.Models.WellCastServerContext>());

        public WellCastServerContext() : base("name=WellCastServerContext")
        {
        }

        public DbSet<Symptom> WellCastSymptoms { get; set; }

        public DbSet<Condition> WellCastConditions { get; set; }

        public DbSet<SymptomCategory> WellCastSymptomCategories { get; set; }

        public DbSet<ConditionSymptom> WellCastConditionSymptoms { get; set; }

        public DbSet<WellCastLog> WellCastLogs { get; set; }

        public DbSet<Location> WellCastLocations { get; set; }

        public DbSet<ConditionProfile> WellCastConditionProfiles { get; set; }

        public DbSet<Profile> WellCastProfiles { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
