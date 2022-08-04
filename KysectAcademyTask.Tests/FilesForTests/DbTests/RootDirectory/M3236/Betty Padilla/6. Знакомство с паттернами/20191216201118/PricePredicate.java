package filter;

import entity.Product;

public class PricePredicate extends Predicate
{
	public float minPrice = -1;
	public float maxPrice = Float.MAX_VALUE;

	public PricePredicate() {
	}

	public PricePredicate(float minPrice, float maxPrice) {
		this.minPrice = minPrice;
		this.maxPrice = maxPrice;
	}

	@Override
	public boolean check(Product product) {
		return (product.getPrice() >= minPrice) && (product.getPrice() <= maxPrice);
	}
}
