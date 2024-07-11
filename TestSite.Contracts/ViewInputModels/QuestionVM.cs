namespace TestSite.Contracts.ViewInputModels;

public class QuestionVM
{
	public bool HasMultipleAnswers { get; set; }
	public string Text { get; set; } = string.Empty;
	public List<QuestionOptionVM> QuestionOptions { get; set; } = new ();
}