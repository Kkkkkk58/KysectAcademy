package entity;

public class GPU
{
	String model;
	String vendor;

	@Override
	public String toString() {
		return "GPU{" +
				"model='" + model + '\'' +
				", vendor='" + vendor + '\'' +
				'}';
	}

	public GPU(String model, String vendor) {
		this.model = model;
		this.vendor = vendor;
	}
}
