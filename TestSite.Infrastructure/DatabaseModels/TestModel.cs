using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestSite.Domain;

namespace TestSite.Infrastructure.DatabaseModels;

public class TestModel
{
	public Guid Id { get; set; }
	
	public string Title { get; set; } 

	public string Description { get; set; }
	
	[Column("Questions")]
	public List<QuestionModel> Questions { get; set; }
	
	public TimeSpan TimeSpan { get; set; }
	
	[Required]
	public User User { get; set; }
}