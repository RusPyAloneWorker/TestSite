namespace TestSite.Contracts.ViewModels;

public class QuestionOptionVm
{
	public Guid Id { get; set; }
	
	public bool? IsCorrect { get; set; }
	
	public string? Text { get; set; }
}