using TestSite.Contracts;
using TestSite.Contracts.ViewInputModels;
using TestEntity = TestSite.Domain.TestRoot.Test;

namespace TestSite.Domain.Abstractions;

/// <summary>
/// Репозиторий тестов.
/// </summary>
public interface ITestRepository
{
	/// <summary>
	/// Добавить новый тест.
	/// </summary>
	/// <param name="testVm">Тест.</param>
	/// <param name="userId">Пользователь, создавший тест.</param>
	/// <returns>Результат добавления теста с идентификатором новго теста.</returns>
	Task<Result<Guid>> AddTestAsync(TestVM testVm, Guid userId);

	/// <summary>
	/// Редактировать тест.
	/// </summary>
	/// <param name="newQuestions">Новые вопросы.</param>
	/// <param name="testId">Идентификатор теста.</param>
	/// <returns>Результат изменения.</returns>
	Result EditTestAsync(List<QuestionVM> newQuestions, Guid testId);

	/// <summary>
	/// Удалить тест.
	/// </summary>
	/// <param name="testId">Идентификатор теста.</param>
	/// <returns>Результат удаления.</returns>
	Result DeleteTestAsync(Guid testId);

	/// <summary>
	/// Получить все тесты.
	/// </summary>
	/// <returns>Результат получения тестов со списком тестов.</returns>
	Task<Result<List<TestEntity>>> GetTestsAsync();

	/// <summary>
	/// Получить тест по идентификатору.
	/// </summary>
	/// <param name="testId">Идентификатор теста.</param>
	/// <returns>Результат получения теста.</returns>
	Task<Result<TestEntity>> GetTestByIdAsync(Guid testId);
}