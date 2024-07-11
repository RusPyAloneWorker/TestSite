using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestSite.Contracts;
using TestSite.Contracts.ViewInputModels;
using TestSite.Domain.Abstractions;
using TestSite.Domain.TestResultRoot;
using TestSite.Infrastructure.DatabaseModels;

namespace TestSite.Infrastructure.Repositories;

public class TestResultRepository : ITestResultRepository
{
	private readonly AppDbContext _dbContext;
	private readonly IMapper _mapper;

	public TestResultRepository(AppDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}
	
	public async Task<Result<Guid>> AddTestResultAsync(TestResultVM testResultVm, Guid userId)
	{
		var id = Guid.NewGuid();

		try
		{
			var testResult = new TestResult(id, testResultVm.TestId);
			var answeredQuestions =
				_mapper.Map<List<AnsweredQuestionVM>, List<AnsweredQuestion>>(testResultVm.AnsweredQuestions)
					.Select(x => (x.Question, x.QuestionOptions))
					.ToList();

			var result = testResult.AddAnsweredQuestions(answeredQuestions);

			if (result.IsFailure)
			{
				return new Result<Guid>(Guid.Empty, false, result.Error);
			}

			var testResultModel = _mapper.Map<TestResult, TestResultModel>(testResult);

			foreach (var answeredQuestion in testResultModel.AnsweredQuestions)
			{
				var guids = answeredQuestion.QuestionOptions.Select(x => x.Id);
				
				answeredQuestion.Question.QuestionOptions = null;
				answeredQuestion.QuestionOptions =
					_dbContext.QuestionOptions.Where(x => guids.Any(x1 => x1 == x.Id)).ToList();
				answeredQuestion.Question = null;
			}
			

			await _dbContext.TestResults.AddAsync(testResultModel);
			await _dbContext.SaveChangesAsync();
		}
		catch (ArgumentException ae)
		{
			return new Result<Guid>(Guid.Empty, false, ae.Message);
		}
		catch (Exception e)
		{
			throw new SystemException("Couldn't add test result", e);
		}

		return new Result<Guid>(id, true);
	}

	public async Task<Result<TestResult>> GetTestResultByIdAsync(Guid testId)
	{
		var testResultModel = await _dbContext
			.TestResults
			.Include(x =>x.Test)
			.Include(x => x.AnsweredQuestions)
			.ThenInclude(x => x.Question)
			.Include(x => x.AnsweredQuestions)
			.ThenInclude(x => x.QuestionOptions)
			.FirstOrDefaultAsync(x => x.TestId == testId);
		var testResult = _mapper.Map<TestResultModel, TestResult>(testResultModel!);

		return new Result<TestResult>(testResult, true);
	}

	public Result<List<TestResult>> GetTestResultsByUserIdAsync(Guid userId)
	{
		throw new NotImplementedException();
	}

	public async Task<Result<TestCompletionStatus?>> GetTestCompletionStatusAsync(Guid userId, Guid testId)
	{
		var testCompletionStatusModel = await _dbContext.TestCompletionStatuses
			.Include(x => x.User)
			.Include(x => x.Test)
			.ThenInclude(x => x.User)
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.TestId == testId && x.UserId == userId.ToString());

		if (testCompletionStatusModel is null)
		{
			return new Result<TestCompletionStatus?>(null, true, "TestCompletionStatus not found");
		}

		var testCompletionStatus = _mapper.Map<TestCompletionStatusModel, TestCompletionStatus>(testCompletionStatusModel);

		return new Result<TestCompletionStatus?>(testCompletionStatus, true);
	}

	public async Task<Result> UpdateTestCompletionStatusAsync(TestCompletionStatus testCompletionStatus)
	{
		try
		{
			var testCompletionStatusModel =
				_mapper.Map<TestCompletionStatus, TestCompletionStatusModel>(testCompletionStatus);

			_dbContext.TestCompletionStatuses.Update(testCompletionStatusModel);
			await _dbContext.SaveChangesAsync();
			
			return Result.Success;
		}
		catch (Exception e)
		{
			return new Result(false, e.Message);
		}
	}

	public async Task<Result<Guid>> CreateTestCompletionStatusAsync(Guid userId, Guid testId)
	{
		var testStatus = new TestCompletionStatus(userId.ToString(), testId);

		var testCompletionStatusModel = _mapper.Map<TestCompletionStatus, TestCompletionStatusModel>(testStatus);

		await _dbContext.TestCompletionStatuses.AddAsync(testCompletionStatusModel);
		await _dbContext.SaveChangesAsync();

		return new Result<Guid>(testStatus.Id, true);
	}
}