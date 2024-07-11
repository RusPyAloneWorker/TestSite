using TestSite.Contracts.ViewModels;

namespace TestSite.Contracts.ViewInputModels;

public class AnsweredQuestionVM
{
	public List<QuestionOptionVm> QuestionOptions { get; set; }
	
	public QuestionVm Question { get; set; }
}