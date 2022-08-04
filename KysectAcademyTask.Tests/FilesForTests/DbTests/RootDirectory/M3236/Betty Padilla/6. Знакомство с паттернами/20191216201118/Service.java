package service;

import entity.Product;
import filter.Predicate;

import java.util.ArrayList;
import java.util.List;

public class Service
{
	public static final Service instance = new Service();

	private List<Product> products = new ArrayList<>();

	public void add(Product product)
	{
		products.add(product);
	}

	public List<Product> find(Predicate predicate)
	{
		List<Product> res = new ArrayList<>();
		for (Product p: products)
			if (predicate.check(p))
				res.add(p);

		return res;
	}
}
