using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestSite.Contracts.ViewInputModels;
using TestSite.Domain.Abstractions;

namespace TestSite.Pages;

public class TestResultModel : PageModel
{
	private readonly ITestResultRepository _testResultRepository;
	private readonly ITestRepository _testRepository;
	private readonly IMapper _mapper;
	
	public TestResultVM Result { get; set; }
	
	public int RightAnswers { get; set; }

	public TestResultModel(ITestResultRepository testResultRepository, ITestRepository testRepository, IMapper mapper)
	{
		_testResultRepository = testResultRepository;
		_testRepository = testRepository;
		_mapper = mapper;
	}
	
	public async Task OnGet([FromQuery] Guid testResultId)
	{
		var resultModel = await _testResultRepository.GetTestResultByIdAsync(testResultId);

		if (resultModel.IsFailure)
		{
			ModelState.AddModelError("", "");
		}
		
		if (ModelState.IsValid)
		{
			Result = _mapper.Map<Domain.TestResultRoot.TestResult, TestResultVM>(resultModel.Value!);
			var test = (await _testRepository.GetTestByIdAsync(Result.TestId)).Value!;

			foreach (var question in test.Questions)
			{
				var answeredQuestion = Result.AnsweredQuestions
					.FirstOrDefault(x => x.Question.Id == question.Id)!;

				var rightAnswers = question.Answers.Where(x => x.IsCorrect);

				if (rightAnswers.Count() != answeredQuestion.QuestionOptions.Count)
				{
					continue;
				}

				var isUserAnswerCorrect = true;
				
				foreach (var answer in question.Answers.Where(x => x.IsCorrect))
				{
					var rightAnswerExists = answeredQuestion.QuestionOptions?.Single(x => x.Id == answer.Id) is not null;

					if (!rightAnswerExists)
					{
						isUserAnswerCorrect = false;
					}
				}

				if (isUserAnswerCorrect)
				{
					RightAnswers++;
				}
			}
		}
	}
}