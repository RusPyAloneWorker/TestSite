using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestSite.Infrastructure.DatabaseModels;

public class AnsweredQuestionModel
{
	[Key]
	public Guid Id { get; set; }
	
	[Column("Column")]
	public QuestionModel Question { get; set; }
	
	public Guid QuestionId { get; set; }
	
	[Column("QuestionOptions")]
	public List<QuestionOptionModel> QuestionOptions { get; set; }
}