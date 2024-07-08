using System.ComponentModel.DataAnnotations.Schema;

namespace TestSite.Infrastructure.DatabaseModels;

public class QuestionModel
{
	public Guid Id { get; set; }

	public bool HasMultipleAnswers { get; set; } = true;
	
	public string Text { get; set; }
	
	[Column("QuestionOptions")]
	public List<QuestionOptionModel> QuestionOptions { get; set; }

	[Column("Test")]
	public TestModel Test { get; set; }
}