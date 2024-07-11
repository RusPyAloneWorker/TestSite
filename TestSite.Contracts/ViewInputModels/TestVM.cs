using System.ComponentModel.DataAnnotations;

namespace TestSite.Contracts.ViewInputModels;

public class TestVM
{
	[Required(ErrorMessage = "Required")]
	public string Title { get; set; } 

	[Required(ErrorMessage = "Required")]
	public string Description { get; set; }
	
	[Required(ErrorMessage = "Questions are empty")]
	public List<QuestionVM> Questions { get; set; }
	
	[Required(ErrorMessage = "Required")]
	public int TimeSpan { get; set; }
}