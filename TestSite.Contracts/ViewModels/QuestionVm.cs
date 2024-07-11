namespace TestSite.Contracts.ViewModels;

/// <summary>
/// ViewModel вопроса для вывода пользователю.
/// </summary>
public class QuestionVm
{
	public Guid Id { get; set; }
	
	public bool HasMultipleAnswers { get; set; }
	
	public string Text { get; set; } = string.Empty;
	
	public List<QuestionOptionVm> QuestionOptions { get; set; } = new ();
}