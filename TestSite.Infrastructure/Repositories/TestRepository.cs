using AutoMapper;
using TestSite.Contracts;
using TestSite.Contracts.ViewModels;
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

	public async Task<Result<Guid>> AddTest(TestVM testVm)
	{
		var guid = Guid.NewGuid();

		try
		{
			var test = new Test(testVm, guid);
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

	public Result EditTest(List<QuestionVM> newQuestions, Guid testId)
	{
		throw new NotImplementedException();
	}

	public Result DeleteTest(Guid testId)
	{
		throw new NotImplementedException();
	}

	public Result<List<Test>> GetTests()
	{
		throw new NotImplementedException();
	}

	public Result<Test> GetTestById(Guid testId)
	{
		throw new NotImplementedException();
	}
}