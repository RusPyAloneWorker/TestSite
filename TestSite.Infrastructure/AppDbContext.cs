using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestSite.Domain;
using TestSite.Infrastructure.DatabaseModels;

namespace TestSite.Infrastructure;

public class AppDbContext: IdentityDbContext<User>
{
	public DbSet<TestModel> Tests { get; set; }
	
	public DbSet<QuestionOptionModel> QuestionOptions { get; set; }

	public DbSet<QuestionModel> Questions { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{ }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<User>().Property(x => x.Name).HasMaxLength(30);

		builder.Entity<TestModel>()
			.HasMany(x => x.Questions)
			.WithOne(x => x.Test);

		builder.Entity<QuestionModel>()
			.HasMany(x => x.QuestionOptions)
			.WithOne(x => x.Question);
	}
}