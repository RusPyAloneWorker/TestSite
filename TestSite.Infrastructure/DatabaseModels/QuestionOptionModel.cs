namespace TestSite.Infrastructure.DatabaseModels;

public class QuestionOptionModel
{
	public Guid Id { get; set; }

	public string Text { get; set; }
	
	public bool IsCorrect { get; set; }

	public QuestionModel Question { get; set; }
}