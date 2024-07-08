using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestSite.Contracts.ViewModels;
using TestSite.Domain.Abstractions;

namespace TestSite.Pages;

[IgnoreAntiforgeryToken]
public class CreateTestModel : PageModel
{
	private readonly ILogger<ErrorModel> _logger;
	private readonly IMapper _mapper;
	private readonly ITestRepository _testRepository;

	[BindProperty]
	[Required(ErrorMessage = "")]
	public TestVM Test { get; set; }

	public CreateTestModel(
		ILogger<ErrorModel> logger, 
		IMapper mapper,
		ITestRepository testRepository)
	{
		_logger = logger;
		_mapper = mapper;
		_testRepository = testRepository;
	}

	public void OnGet()
	{
		
	}

	public async Task OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			return;
		}
		
		var noRightAnswerQuestion = Test.Questions
				.Where(x =>
				{
					var options = x.QuestionOptions;
					return options.All(x => !x.IsCorrect);
				}); 
		
		if (noRightAnswerQuestion.Any())
		{
			ModelState.AddModelError("Test.Questions", "No right answers was chosen.");
		}

		var isOneCorrectAnswerQuestionHasMultipleCorrectOptions = Test.Questions
			?.Where(x => !x.HasMultipleAnswers)
			.Select(x => x.QuestionOptions)
			.Select(x => x.Count(x => x.IsCorrect))
			.Any(x => x > 1);
		
		if (isOneCorrectAnswerQuestionHasMultipleCorrectOptions is null || isOneCorrectAnswerQuestionHasMultipleCorrectOptions.Value)
		{
			ModelState.AddModelError(
				"Test.Questions", 
				@"Some question has multiple correct options but checkbox ""Can have multiple answers"" was not toggled"
			);
		}
		
		if (ModelState.IsValid)
		{
			await _testRepository.AddTest(Test);	
		}
	}
}