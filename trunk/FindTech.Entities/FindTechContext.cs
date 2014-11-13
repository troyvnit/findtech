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
        DbSet<ArticleCategory> ArticleCategories { get; set; }
        DbSet<Benchmark> Benchmarks { get; set; }
        DbSet<BenchmarkGroup> BenchmarkGroups { get; set; }
        DbSet<BenchmarkDetail> BenchmarkDetails { get; set; }
        DbSet<Device> Devices { get; set; }
        DbSet<Brand> Brands { get; set; }
        DbSet<Spec> Specs { get; set; }
        DbSet<SpecDetail> SpecDetails { get; set; }
        DbSet<SpecGroup> SpecGroups { get; set; }
    }
}
