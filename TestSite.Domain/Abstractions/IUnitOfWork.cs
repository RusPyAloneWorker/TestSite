namespace TestSite.Domain.Abstractions;

public interface IUnitOfWork
{
	Task SaveChangesAsync(CancellationToken? cancellationToken = null);
}