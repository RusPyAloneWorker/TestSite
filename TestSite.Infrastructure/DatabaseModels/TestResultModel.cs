using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestSite.Domain;

namespace TestSite.Infrastructure.DatabaseModels;

public class TestResultModel
{
	public User User { get; set; }
	
	[Column("AnsweredQuestions")]
	public List<AnsweredQuestionModel> AnsweredQuestions { get; set; }
	
	[Key]
	public Guid Id { get; set; }
	
	public TestModel Test { get; set; }
	
	[ForeignKey(nameof(Test))]
	public Guid TestId { get; set; } 
}