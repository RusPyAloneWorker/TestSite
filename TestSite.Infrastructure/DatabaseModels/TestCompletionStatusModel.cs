using TestSite.Infrastructure.DatabaseModels;

namespace TestSite.Domain.TestResultRoot;

public class TestCompletionStatusModel
{
	public Guid Id { get; set; }
	
	public string UserId { get; set; }
	
	public User User { get; set; }
	
	public Guid TestId { get; set; }
	
	public TestModel Test { get; set; }
	
	public TimeSpan TimePassed { get; set; }
	
	public int Retries { get; set; } 
	
	public bool IsOver { get; set; }
}