namespace TestSite.Domain.Abstractions;

/// <summary>
/// Единица работы.
/// </summary>
public interface IUnitOfWork
{
	/// <summary>
	/// Сохранить все изменеия.
	/// </summary>
	/// <param name="cancellationToken">Токен отмены сохранения.</param>
	/// <returns>Задача.</returns>
	Task SaveChangesAsync(CancellationToken? cancellationToken = null);
}