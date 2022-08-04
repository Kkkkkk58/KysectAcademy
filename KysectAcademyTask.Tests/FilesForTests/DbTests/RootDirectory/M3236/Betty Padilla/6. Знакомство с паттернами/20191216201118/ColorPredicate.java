package filter;

import entity.Product;

public class ColorPredicate extends Predicate
{
	public String color;

	public ColorPredicate(String color) {
		this.color = color;
	}

	@Override
	public boolean check(Product product) {
		return product.getColor().equals(color);
	}
}
