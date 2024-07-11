using System.ComponentModel.DataAnnotations;

namespace TestSite.Contracts.ViewInputModels;

public class TestResultVM
{
	[Required]
	public List<AnsweredQuestionVM> AnsweredQuestions { get; set; }
	
	[Required]
	public Guid TestId { get; set; }
}