package entity;

public class CPU
{
	String model;
	String vendor;
	double clockrate;

	@Override
	public String toString() {
		return "CPU{" +
				"model='" + model + '\'' +
				", vendor='" + vendor + '\'' +
				", clockrate=" + clockrate +
				'}';
	}

	public CPU(String model, String vendor, double clockrate) {
		this.model = model;
		this.vendor = vendor;
		this.clockrate = clockrate;
	}
}
