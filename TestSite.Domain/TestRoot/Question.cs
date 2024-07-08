using TestSite.Domain.Primitives;

namespace TestSite.Domain.TestRoot;

public class Question : Entity
{
	public bool HasMultipleAnswers { get; private set; } = true;
	public string Text { get; private set; }
	public List<QuestionOption> Answers { get; private set; } 

	internal Question(string text, List<QuestionOption> answers, Guid id, bool hasMultipleAnswers = true) 
		: base(id)
	{
		if (answers is null || answers.Count == 0)
		{
			throw new ArgumentException("Options are empty");
		}

		var rightAnswers = answers.Where(x => x.IsCorrect);

		if (!rightAnswers.Any())
		{
			throw new ArgumentException("No right answers");
		}

		if (!hasMultipleAnswers && rightAnswers.Count() > 1)
		{
			throw new SystemException("Question has only one answer, but multiple right answers were provided");
		}
		
		if (string.IsNullOrWhiteSpace(text))
		{
			throw new ArgumentException("Text of question is empty");
		}
		
		HasMultipleAnswers = hasMultipleAnswers;
		Text = text;
		Answers = answers;
	}
}