namespace TestSite.Domain.Primitives;

/// <summary>
/// Сущность.
/// </summary>
public abstract class Entity
{
	/// <summary>
	/// Идентификатор.
	/// </summary>
	public Guid Id { get; private set; }
	
	protected Entity(Guid id)
	{
		if (id == Guid.Empty)
		{
			throw new ArgumentException("Id is empty");
		}
		
		Id = id;
	}
}