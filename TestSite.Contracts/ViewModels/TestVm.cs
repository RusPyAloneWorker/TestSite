using System.ComponentModel.DataAnnotations;

namespace TestSite.Contracts.ViewModels;

public class TestVm
{
	public Guid Id { get; set; }
	
	public string Title { get; set; } 

	public string Description { get; set; }
	
	public List<QuestionVm> Questions { get; set; }
	
	public TimeSpan TimeSpan { get; set; }
}