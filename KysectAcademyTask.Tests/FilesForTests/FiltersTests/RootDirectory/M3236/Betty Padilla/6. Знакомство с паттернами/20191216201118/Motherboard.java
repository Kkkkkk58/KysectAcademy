package entity;

public class Motherboard
{
	String model;
	String vendor;

	public Motherboard(String model, String vendor) {
		this.model = model;
		this.vendor = vendor;
	}

	@Override
	public String toString() {
		return "Motherboard{" +
				"model='" + model + '\'' +
				", vendor='" + vendor + '\'' +
				'}';
	}
}
