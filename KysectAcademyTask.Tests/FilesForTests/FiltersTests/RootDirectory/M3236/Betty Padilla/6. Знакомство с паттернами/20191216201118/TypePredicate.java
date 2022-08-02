package filter;

import entity.Product;

public class TypePredicate extends Predicate
{
	public Product.Type type;

	public TypePredicate(Product.Type type) {
		this.type = type;
	}

	@Override
	public boolean check(Product product) {
		return product.getType().equals(type);
	}
}
