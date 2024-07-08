using TestSite.Domain.Primitives;

namespace TestSite.Domain.TestRoot;

public class QuestionOption: Entity
{
	public string Text { get; private set; }
	
	public bool IsCorrect { get; private set; }
	
	internal QuestionOption(string text, bool isCorrect, Guid id) 
		: base(id)
	{
		if (string.IsNullOrWhiteSpace(text))
		{
			throw new ArgumentException("Question option's text is empty");
		}

		IsCorrect = isCorrect;
		Text = text;
	}
}