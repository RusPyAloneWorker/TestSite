﻿namespace TestSite.Domain.Primitives;

public abstract class AggregateRoot : Entity
{
	protected AggregateRoot(Guid guid) 
		: base(guid)
	{ }
}