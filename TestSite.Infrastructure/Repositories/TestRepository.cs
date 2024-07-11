using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestSite.Contracts;
using TestSite.Contracts.ViewInputModels;
using TestSite.Domain;
using TestSite.Domain.Abstractions;
using TestSite.Domain.TestRoot;
using TestSite.Infrastructure.DatabaseModels;

namespace TestSite.Infrastructure.Repositories;

public class TestRepository: ITestRepository
{
	private readonly AppDbContext _dbContext;
	private readonly IMapper _mapper;

	public TestRepository(AppDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public async Task<Result<Guid>> AddTestAsync(TestVM testVm, Guid userId)
	{
		var guid = Guid.NewGuid();

		try
		{
			var user = await _dbContext
				.Users
				.FirstAsync(x => x.Id == userId.ToString());

			if (user is null)
			{
				return new Result<Guid>(default, false, "User was not found");
			}

			var test = new Test(testVm, guid, userId, user);
			var result = test.AddQuestions(testVm.Questions);

			if (result.IsFailure)
			{
				return new Result<Guid>(Guid.Empty, false, result.Error);
			}
			
			var testModel = _mapper.Map<Test, TestModel>(test);
			_dbContext.Tests.Add(testModel);
			await _dbContext.SaveChangesAsync();
		}
		catch (ArgumentException ae)
		{
			return new Result<Guid>(default, false, ae.Message);
		}
		catch (Exception e)
		{
			throw new ApplicationException("Unable to add new test", e);
		}

		return new Result<Guid>(guid, true);
	}

	public Result EditTestAsync(List<QuestionVM> newQuestions, Guid testId)
	{
		throw new NotImplementedException();
	}

	public Result DeleteTestAsync(Guid testId)
	{
		throw new NotImplementedException();
	}

	public async Task<Result<List<Test>>> GetTestsAsync()
	{
		var testModels = await _dbContext.Tests
			.AsNoTracking()
			.Include(x => x.User)
			.Include(x => x.Questions)
			.ThenInclude(x => x.QuestionOptions)
			.AsNoTracking()
			.ToListAsync();

		var tests = _mapper.Map<List<TestModel>, List<Test>>(testModels);

		return new Result<List<Test>>(tests, true);
	}

	public async Task<Result<Test>> GetTestByIdAsync(Guid testId)
	{
		var testModel = await _dbContext.Tests
			.AsNoTracking()
			.Include(x=>x.User)
			.Include(x => x.Questions)
			.ThenInclude(x => x.QuestionOptions)
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Id == testId);

		if (testModel is null)
		{
			return new Result<Test>(null, false, "Test not found");
		}
		
		var test = _mapper.Map<TestModel, Test>(testModel);

		return new Result<Test>(test, true);
	}
}