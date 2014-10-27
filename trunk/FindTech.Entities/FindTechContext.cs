using System.Data.Entity;
using FindTech.Entities.Migrations;
using FindTech.Entities.Models;
using Repository.Pattern.Ef6;

namespace FindTech.Entities
{
    public class FindTechContext : DataContext
    {
        public FindTechContext()
            : base("Name=FindTechContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FindTechContext, Configuration>());
        }

        DbSet<Article> Articles { get; set; }
    }
}
