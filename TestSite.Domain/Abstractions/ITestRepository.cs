using TestSite.Contracts;
using TestSite.Contracts.ViewInputModels;
using TestEntity = TestSite.Domain.TestRoot.Test;

namespace TestSite.Domain.Abstractions;

public interface ITestRepository
{
	Task<Result<Guid>> AddTestAsync(TestVM testVm, Guid userId);

	Result EditTestAsync(List<QuestionVM> newQuestions, Guid testId);

	Result DeleteTestAsync(Guid testId);

	Task<Result<List<TestEntity>>> GetTestsAsync();

	Task<Result<TestEntity>> GetTestByIdAsync(Guid testId);
}