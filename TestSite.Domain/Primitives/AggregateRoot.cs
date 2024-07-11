namespace TestSite.Domain.Primitives;

/// <summary>
/// Aggregate Root.
/// </summary>
public abstract class AggregateRoot : Entity
{
	protected AggregateRoot(Guid guid) 
		: base(guid)
	{ }
}