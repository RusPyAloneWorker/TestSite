using TestSite.Contracts;
using TestSite.Contracts.ViewInputModels;
using TestSite.Domain.TestResultRoot;

namespace TestSite.Domain.Abstractions;

/// <summary>
/// Репозиторий результатов прохождения теста.
/// </summary>
public interface ITestResultRepository
{ 
	/// <summary>
	/// Добавить результат прохождения теста.
	/// </summary>
	/// <param name="testResultVm">Результат прохождения теста.</param>
	/// <param name="userId">Идентификатор пользователя.</param>
	/// <returns>Результат с идентификатором результата прохождения теста.</returns>
	Task<Result<Guid>> AddTestResultAsync(TestResultVM testResultVm, Guid userId);

	/// <summary>
	/// Получить результат прохождения теста по идентификатору.
	/// </summary>
	/// <param name="testId">Идентификатор результата прохождения теста.</param>
	/// <returns>Данные о результате прохождения теста.</returns>
	Task<Result<TestResult>> GetTestResultByIdAsync(Guid testId);

	/// <summary>
	/// Получить результаты прохождения тестов от пользователя.
	/// </summary>
	/// <param name="userId">Идентификатор пользователя.</param>
	/// <returns>Список результатов пройденных тестов</returns>
	Result<List<TestResult>> GetTestResultsByUserIdAsync(Guid userId);

	/// <summary>
	/// Получить статус прохождения теста.
	/// </summary>
	/// <param name="userId">Идентификатор пользователя.</param>
	/// <param name="testId">Идентификатор теста.</param>
	/// <returns>Результат со статусом прохождения теста.</returns>
	Task<Result<TestCompletionStatus?>> GetTestCompletionStatusAsync(Guid userId, Guid testId);

	/// <summary>
	/// Обновить данные статуса прохождения теста.
	/// </summary>
	/// <param name="testCompletionStatus">Статус прохождения теста.</param>
	/// <returns>Результат операции.</returns>
	Task<Result> UpdateTestCompletionStatusAsync(TestCompletionStatus testCompletionStatus);

	/// <summary>
	/// Создать статус прохождения теста.
	/// </summary>
	/// <param name="userId">Идентификатор пользователя.</param>
	/// <param name="testId">Идентификатор теста.</param>
	/// <returns>Результат с идентификатором созданного статуса.</returns>
	Task<Result<Guid>> CreateTestCompletionStatusAsync(Guid userId, Guid testId);
}