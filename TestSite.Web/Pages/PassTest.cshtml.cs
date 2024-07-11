using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestSite.Contracts.ViewModels;
using TestSite.Domain.Abstractions;
using TestSite.Domain.TestRoot;

namespace TestSite.Pages;

[Authorize]
public class PassTestModel : PageModel
{
	private readonly ITestRepository _testRepository;
	private readonly ITestResultRepository _testResultRepository;
	private readonly IMapper _mapper;
	public TestVm Test { get; set; }

	[BindProperty] public TestResultVM TestResult { get; set; }

	public PassTestModel(ITestRepository testRepository, ITestResultRepository testResultRepository, IMapper mapper)
	{
		_testRepository = testRepository;
		_testResultRepository = testResultRepository;
		_mapper = mapper;
	}

	public async Task OnGetAsync([FromQuery] Guid testId)
	{
		var result = await _testRepository.GetTestByIdAsync(testId);

		if (result.IsFailure)
		{
			return;
		}

		Test = _mapper.Map<Test, TestVm>(result.Value!);

		var userId = new Guid(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
		var testStatus = await _testResultRepository.GetTestCompletionStatusAsync(userId, testId);

		if (testStatus.IsFailure || testStatus.Value is null)
		{
			await _testResultRepository.CreateTestCompletionStatusAsync(userId, testId);
		}
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		for (var i = 0; i < TestResult.AnsweredQuestions.Count; i++)
		{
			if (TestResult.AnsweredQuestions[i].QuestionOptions.All(x => !x.IsChecked))
			{
				ModelState.AddModelError($"TestResult.AnsweredQuestions[{i}]", "No option was selected");
			}
		}

		var testByIdResult = await _testRepository.GetTestByIdAsync(TestResult.TestId);

		if (testByIdResult.IsFailure)
		{
			return Page();
		}

		Test = _mapper.Map<Test, TestVm>(testByIdResult.Value!);
		
		if (ModelState.IsValid)
		{
			var testResultVM = new Contracts.ViewInputModels.TestResultVM();
			testResultVM.TestId = TestResult.TestId;
			testResultVM.AnsweredQuestions = new List<Contracts.ViewInputModels.AnsweredQuestionVM>();

			foreach (var elem in TestResult.AnsweredQuestions)
			{
				var question = Test
					.Questions
					.FirstOrDefault(x => x.Id == elem.QuestionId)!;
				
				var answeredQuestionVm = new Contracts.ViewInputModels.AnsweredQuestionVM();
				answeredQuestionVm.Question = new QuestionVm();
				answeredQuestionVm.Question.HasMultipleAnswers = question.HasMultipleAnswers;
				answeredQuestionVm.Question.Id = question.Id;
				
				// Выбираю из теста только выделенные пункты (ответы)
				var selectedTestResultOptions = elem
					.QuestionOptions
					.Where(x => x.IsChecked)
					.Select(x => x.Id).ToList();
				var selectedTestOptions = question
					.QuestionOptions
					.Where(x => selectedTestResultOptions.Contains(x.Id))
					.ToList();

				if (selectedTestOptions is null || !selectedTestOptions.Any())
				{
					throw new Exception();
				}
				
				answeredQuestionVm.QuestionOptions = selectedTestOptions;
				answeredQuestionVm.Question.QuestionOptions.AddRange(selectedTestOptions);
				testResultVM.AnsweredQuestions.Add(answeredQuestionVm);
			}

			var userId = User.Claims
				.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
				?.Value ?? throw new NullReferenceException();
			
			var result = await _testResultRepository.AddTestResultAsync(testResultVM, new Guid(userId));
			
			var url = Url.Page("/TestResult", new { testResultId = Test.Id });
			return Redirect(url);
		}

		return Page();
	}

	public class TestResultVM
	{
		[Required]
		public List<AnsweredQuestionVM> AnsweredQuestions { get; set; }
	
		[Required]
		public Guid TestId { get; set; }
	}

	public class AnsweredQuestionVM
	{
		[Required]
		public List<QuestionOptionVM> QuestionOptions { get; set; }
	
		[Required]
		public Guid QuestionId { get; set; }
	}

	public class QuestionOptionVM
	{
		[Required]
		public bool IsChecked { get; set; }
		
		[Required]
		public Guid Id { get; set; }
	}
}