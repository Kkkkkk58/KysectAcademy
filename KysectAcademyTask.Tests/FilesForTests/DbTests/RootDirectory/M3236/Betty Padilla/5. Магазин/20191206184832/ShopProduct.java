package entity;

public class ShopProduct
{
	public final int shopID;
	public final String product;
	public final float price;
	public final int amount;

	public ShopProduct(int shopID, String product, float price, int amount) {
		this.shopID = shopID;
		this.product = product;
		this.price = price;
		this.amount = amount;
	}
}
