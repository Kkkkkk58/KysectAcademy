package service;

import dao.*;
import entity.Shop;
import entity.ShopProduct;
import javafx.util.Pair;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.util.List;
import java.util.Properties;

public class Service
{
	private ShopDAO sdao;
	private ShopProductDAO spdao;

	private Service()
	{
		boolean useCSV = false;

		File file = new File("config.properties");
		Properties properties = new Properties();

		try
		{
			FileInputStream fis = new FileInputStream("config.properties");
			properties.load(fis);

			useCSV = Boolean.parseBoolean(properties.getProperty("useCSV"));

		} catch (IOException e)
		{
			e.printStackTrace();
		}

		if (useCSV)
		{
			System.out.println("Using CSV");

			sdao = new ShopDAO_IMP_CSV();
			spdao = new ShopProductDAO_IMP_CSV();
		}
		else
		{
			System.out.println("Using DB");

			sdao = new ShopDAO_IMP_DB();
			spdao = new ShopProductDAO_IMP_DB();
		}

	}


	public void createShop(int id, String name, String address)
	{
		Shop shop = new Shop(id, name, address);
		sdao.add(shop);
	}

	public void shipConsignment(int shopID, String product, int amount, float price)
	{
		ShopProduct sp = spdao.getByID(new Pair<Integer, String>(shopID, product));

		ShopProduct nsp = null;
		if (sp != null)
		{
			if (price == -1)
				price = sp.price;

			nsp = new ShopProduct(sp.shopID, sp.product, price, sp.amount + amount);
		}
		else
			nsp = new ShopProduct(shopID, product, price, amount);

		spdao.update(nsp);
	}

	public int findCheapestDeal(String product)
	{
		List<ShopProduct> spList = spdao.getAll();

		ShopProduct best = null;
		for (ShopProduct sp : spList)
		{
			if (sp.product.equals(product) && (best == null || sp.price < best.price))
				best = sp;
		}

		if (best == null)
			return -1;
		else
			return best.shopID;
	}

	private static Service instance = null;

	public static Service getInstance()
	{
		if (instance == null)
			instance = new Service();
		return instance;
	}
}
