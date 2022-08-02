package entity;

public class LiquidCooling
{
	String model;
	String vendor;

	@Override
	public String toString() {
		return "LiquidCooling{" +
				"model='" + model + '\'' +
				", vendor='" + vendor + '\'' +
				'}';
	}

	public LiquidCooling(String model, String vendor) {
		this.model = model;
		this.vendor = vendor;
	}
}
