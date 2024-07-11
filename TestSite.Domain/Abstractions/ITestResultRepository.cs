using TestSite.Contracts;
using TestSite.Contracts.ViewInputModels;
using TestSite.Domain.TestResultRoot;

namespace TestSite.Domain.Abstractions;

public interface ITestResultRepository
{
	Task<Result<Guid>> AddTestResultAsync(TestResultVM testResultVm, Guid userId);

	Task<Result<TestResult>> GetTestResultByIdAsync(Guid testId);

	Result<List<TestResult>> GetTestResultsByUserIdAsync(Guid userId);

	Task<Result<TestCompletionStatus?>> GetTestCompletionStatusAsync(Guid userId, Guid testId);

	Task<Result> UpdateTestCompletionStatusAsync(TestCompletionStatus testCompletionStatus);

	Task<Result<Guid>> CreateTestCompletionStatusAsync(Guid userId, Guid testId);
}