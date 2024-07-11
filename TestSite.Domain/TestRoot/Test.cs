using System.Text.Json;
using TestSite.Contracts;
using TestSite.Contracts.ViewInputModels;
using TestSite.Domain.Primitives;

namespace TestSite.Domain.TestRoot;

/// <summary>
/// Тест.
/// </summary>
public class Test : AggregateRoot
{
	private readonly List<Question> _questions;
	
	#region Constants
	private static readonly TimeSpan MinTimeSpan = new (0, 1, 0);
	
	private static readonly TimeSpan MaxTimeSpan = new (0, 50, 0);
	#endregion

	/// <summary>
	/// Идентификатор создателя теста.
	/// </summary>
	public Guid UserId { get; set; }
	
	/// <summary>
	/// Создатель теста.
	/// </summary>
	public User User { get; set; }
	
	/// <summary>
	/// Название теста.
	/// </summary>
	public string Title { get; private set; } 
	
	/// <summary>
	/// Описание теста.
	/// </summary>
	public string Description { get; private set; }

	/// <summary>
	/// Вопросы.
	/// </summary>
	public IReadOnlyList<Question> Questions => _questions;
	
	/// <summary>
	/// Время на выполнение теста.
	/// </summary>
	public TimeSpan TimeSpan { get; private set; }

	public Test(TestVM testVm, Guid id, Guid userId, User user)
		: this(testVm.Title, testVm.Description, new TimeSpan(0, testVm.TimeSpan, 0), id, userId, user)
	{ }
	
	public Test(string title, string description, TimeSpan timeSpan, Guid id, Guid userId, User user) 
		: base(id)
	{
		if (string.IsNullOrWhiteSpace(title))
		{
			throw new ArgumentException("Title of test is empty");
		}

		if (string.IsNullOrWhiteSpace(description))
		{
			throw new ArgumentException("Description of test is empty");
		}

		if (timeSpan < MinTimeSpan || timeSpan > MaxTimeSpan)
		{
			throw new ArgumentException($"Timer is out of bound. Must be between {MinTimeSpan.Minutes} and {MaxTimeSpan.Minutes} minutes");
		}

		if (userId == Guid.Empty || user is null)
		{
			throw new ArgumentException("No user provided.");
		}

		Title = title;
		Description = description;
		_questions = new List<Question>();
		TimeSpan = timeSpan;
		UserId = userId;
		User = user;
	}

	public Result AddQuestion(QuestionVM questionVm)
	{
		try
		{
			var questionOptions = questionVm.QuestionOptions
				.Select(questionOptionVm =>
					new QuestionOption(questionOptionVm.Text, questionOptionVm.IsCorrect, Guid.NewGuid())
				).ToList();

			var questionId = Guid.NewGuid();

			var question = new Question(
				questionVm.Text,
				questionOptions,
				questionId,
				questionVm.HasMultipleAnswers);

			_questions.Add(question);
		}
		catch (ArgumentException e)
		{
			return new Result(false, e.Message);
		}

		return Result.Success;
	}

	public Result AddQuestions(List<QuestionVM> questionVms)
	{
		foreach (var questionVm in questionVms)
		{
			var result = AddQuestion(questionVm);

			if (!result.IsFailure)
			{
				continue;
			}
			
			var serializedQuestion = JsonSerializer.Serialize(questionVms);
			return new Result(false, $"Error for question: {serializedQuestion} - {result.Error}");
		}

		return Result.Success;
	}
}