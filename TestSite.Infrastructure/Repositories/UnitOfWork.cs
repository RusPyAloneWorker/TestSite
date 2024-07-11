using TestSite.Domain.Abstractions;

namespace TestSite.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext _dbContext;

	public UnitOfWork(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	
	public async Task SaveChangesAsync(CancellationToken? cancellationToken = null)
	{
		await _dbContext.SaveChangesAsync(cancellationToken!.Value);
	}
}