using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestSite.Domain;
using TestSite.Domain.TestResultRoot;
using TestSite.Infrastructure.DatabaseModels;

namespace TestSite.Infrastructure;

public class AppDbContext: IdentityDbContext<User>
{
	public DbSet<TestModel> Tests { get; set; }
	
	public DbSet<QuestionOptionModel> QuestionOptions { get; set; }

	public DbSet<QuestionModel> Questions { get; set; }
	
	public DbSet<TestResultModel> TestResults { get; set; }
	
	public DbSet<AnsweredQuestionModel> AnsweredQuestions { get; set; }
	
	public DbSet<TestCompletionStatusModel> TestCompletionStatuses { get; set; }

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

		builder.Entity<AnsweredQuestionModel>()
			.HasMany(x => x.QuestionOptions)
			.WithMany();
		
		builder.Entity<TestCompletionStatusModel>()
			.HasOne(x => x.Test)
			.WithMany()
			.HasForeignKey(x => x.TestId);
		
		builder.Entity<TestCompletionStatusModel>()
			.HasOne(x => x.User)
			.WithMany()
			.HasForeignKey(x => x.UserId);

		builder.Entity<TestResultModel>()
			.HasMany(x => x.AnsweredQuestions)
			.WithOne();
	}
}