using System.ComponentModel.DataAnnotations.Schema;

namespace TestSite.Infrastructure.DatabaseModels;

public class TestModel
{
	public Guid Id { get; set; }
	
	public string Title { get; set; } 

	public string Description { get; set; }
	
	[Column("Questions")]
	public List<QuestionModel> Questions { get; set; }
	
	public TimeSpan TimeSpan { get; set; }
}