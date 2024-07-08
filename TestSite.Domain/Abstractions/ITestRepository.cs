using TestSite.Contracts;
using TestSite.Contracts.ViewModels;
using TestEntity = TestSite.Domain.TestRoot.Test;

namespace TestSite.Domain.Abstractions;

public interface ITestRepository
{
	Task<Result<Guid>> AddTest(TestVM testVm);

	Result EditTest(List<QuestionVM> newQuestions, Guid testId);

	Result DeleteTest(Guid testId);

	Result<List<TestEntity>> GetTests();

	Result<TestEntity> GetTestById(Guid testId);
}