using Microsoft.EntityFrameworkCore;
using RoadSafetyAPI.Models;

namespace RoadSafetyAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();
    public DbSet<ParentProfile> ParentProfiles => Set<ParentProfile>();
    public DbSet<Quiz> Quizzes => Set<Quiz>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<Correction> Corrections => Set<Correction>();
    public DbSet<StudentAnswer> StudentAnswers => Set<StudentAnswer>();
    public DbSet<Score> Scores => Set<Score>();
    public DbSet<Badge> Badges => Set<Badge>();
    public DbSet<StudentBadge> StudentBadges => Set<StudentBadge>();
    public DbSet<Defi> Defis => Set<Defi>();
    public DbSet<StudentDefi> StudentDefis => Set<StudentDefi>();
    public DbSet<Video> Videos => Set<Video>();
    public DbSet<SafetyTip> SafetyTips => Set<SafetyTip>();
    public DbSet<ParkingZone> ParkingZones => Set<ParkingZone>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // StudentBadge composite key
        modelBuilder.Entity<StudentBadge>()
            .HasKey(sb => new { sb.StudentProfileId, sb.BadgeId });

        // StudentDefi composite key
        modelBuilder.Entity<StudentDefi>()
            .HasKey(sd => new { sd.StudentProfileId, sd.DefiId });

        // User -> StudentProfile one-to-one
        modelBuilder.Entity<User>()
            .HasOne(u => u.StudentProfile)
            .WithOne(sp => sp.User)
            .HasForeignKey<StudentProfile>(sp => sp.UserId);

        // User -> ParentProfile one-to-one
        modelBuilder.Entity<User>()
            .HasOne(u => u.ParentProfile)
            .WithOne(pp => pp.User)
            .HasForeignKey<ParentProfile>(pp => pp.UserId);

        // Quiz -> Question one-to-many
        modelBuilder.Entity<Quiz>()
            .HasMany(q => q.Questions)
            .WithOne(q => q.Quiz)
            .HasForeignKey(q => q.QuizId);

        // Question -> Answer one-to-many
        modelBuilder.Entity<Question>()
            .HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId);

        // Question -> Correction one-to-one
        modelBuilder.Entity<Question>()
            .HasOne(q => q.Correction)
            .WithOne(c => c.Question)
            .HasForeignKey<Correction>(c => c.QuestionId);

        // StudentProfile -> StudentAnswer one-to-many
        modelBuilder.Entity<StudentProfile>()
            .HasMany(sp => sp.StudentAnswers)
            .WithOne()
            .HasForeignKey(sa => sa.StudentProfileId);

        // StudentProfile -> StudentBadge one-to-many
        modelBuilder.Entity<StudentProfile>()
            .HasMany(sp => sp.StudentBadges)
            .WithOne(sb => sb.StudentProfile)
            .HasForeignKey(sb => sb.StudentProfileId);

        // Badge -> StudentBadge one-to-many
        modelBuilder.Entity<Badge>()
            .HasMany(b => b.StudentBadges)
            .WithOne(sb => sb.Badge)
            .HasForeignKey(sb => sb.BadgeId);

        // StudentProfile -> Score one-to-many
        modelBuilder.Entity<StudentProfile>()
            .HasMany(sp => sp.Scores)
            .WithOne(s => s.StudentProfile)
            .HasForeignKey(s => s.StudentProfileId);

        // Defi -> StudentDefi one-to-many
        modelBuilder.Entity<Defi>()
            .HasMany(d => d.StudentDefis)
            .WithOne(sd => sd.Defi)
            .HasForeignKey(sd => sd.DefiId);

        // StudentProfile -> StudentDefi one-to-many
        modelBuilder.Entity<StudentProfile>()
            .HasMany<StudentDefi>()
            .WithOne(sd => sd.StudentProfile)
            .HasForeignKey(sd => sd.StudentProfileId);
    }
}
