package filter;

import entity.Product;

public class AndPredicate extends Predicate
{
	private Predicate first;
	private Predicate second;

	public AndPredicate(Predicate first, Predicate second) {
		this.first = first;
		this.second = second;
	}

	@Override
	public boolean check(Product product) {
		return first.check(product) && second.check(product);
	}
}
