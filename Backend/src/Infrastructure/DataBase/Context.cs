using Domain.Entities.Analysis;
using Domain.Entities.Auth;
using EducationProcessAPI.Domain.Entities;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using EducationProcessAPI.Infrastructure.DataBase.Configurations;
using Infrastructure.DataBase.Configurations;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    static ApplicationContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public DbSet<Teacher> Teachers { get; set; } = null!;

    public DbSet<Group> Groups { get; set; } = null!;

    public DbSet<ArtUnion> ArtUnions { get; set; } = null!;

    public DbSet<ArtDirection> ArtDirections { get; set; } = null!;

    public DbSet<Lesson> Lessons { get; set; } = null!;

    public DbSet<AnalysisCriteria> AnalyzeCriterions { get; set; } = null!;

    public DbSet<CriterionOption> CriterionOptions { get; set; } = null!;

    public DbSet<AnalysisDocument> AnalysisDocuments { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Permission> Permissions { get; set; } = null!;

    public DbSet<Role> Roles { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();   
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TeacherConfiguration());
        modelBuilder.ApplyConfiguration(new ArtUnionConfiguration());
        modelBuilder.ApplyConfiguration(new ArtDirectionConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        InitData(modelBuilder);
    }

    private static void InitData(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Permission>().HasData(
        //            new Permission { Id = Guid.NewGuid(), Action = PermissionAction.Create });
    }
}