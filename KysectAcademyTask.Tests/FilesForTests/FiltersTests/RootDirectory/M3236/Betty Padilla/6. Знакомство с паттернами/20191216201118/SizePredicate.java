package filter;

import entity.Product;

public class SizePredicate extends Predicate
{
	public int size;

	public SizePredicate(int size) {
		this.size = size;
	}

	@Override
	public boolean check(Product product) {
		return product.getSize() == size;
	}
}
