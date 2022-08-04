package filter;

import entity.Product;


public abstract class Predicate
{
	abstract public boolean check(Product product);

	public final Predicate and(Predicate second)
	{
		return new AndPredicate(this, second);
	}

	public final Predicate or(Predicate second)
	{
		return new OrPredicate(this, second);
	}
}
