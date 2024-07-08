namespace TestSite.Domain.Primitives;

public abstract class Entity
{
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