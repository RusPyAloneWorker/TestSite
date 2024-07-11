using TestSite.Domain.Primitives;

namespace TestSite.Domain.TestRoot;

/// <summary>
/// Вариант ответа.
/// </summary>
public class QuestionOption: Entity
{
	/// <summary>
	/// Текст варианта ответа.
	/// </summary>
	public string Text { get; private set; }
	
	/// <summary>
	/// Является ли ответ верным.
	/// </summary>
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
	
	private QuestionOption()
		:base(Guid.NewGuid())
	{ }
}