package entity;

public class Product
{
	private float price;
	private String color;
	private int size;

	public enum Type
	{
		socks,
		shoes,
		jacket,
		shirt,
		suit
	}

	private Type type;

	public Product(float price, String color, int size, Type type) {
		this.price = price;
		this.color = color;
		this.size = size;
		this.type = type;
	}

	public float getPrice() {
		return price;
	}

	public String getColor() {
		return color;
	}

	public float getSize() {
		return size;
	}

	public Type getType() {
		return type;
	}

	@Override
	public String toString() {
		return "Product{" +
				"price=" + price +
				", color='" + color + '\'' +
				", size=" + size +
				", type=" + type +
				'}';
	}
}
