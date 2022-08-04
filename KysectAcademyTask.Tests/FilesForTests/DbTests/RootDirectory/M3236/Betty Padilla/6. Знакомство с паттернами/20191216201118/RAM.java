package entity;

public class RAM
{
	public enum Type
	{
		DDR3,
		DDR4
	}

	Type type;
	String model;
	String vendor;
	int size;

	public RAM(Type type, String model, String vendor, int size) {
		this.type = type;
		this.model = model;
		this.vendor = vendor;
		this.size = size;
	}

	@Override
	public String toString() {
		return "RAM{" +
				"type=" + type +
				", model='" + model + '\'' +
				", vendor='" + vendor + '\'' +
				", size=" + size +
				'}';
	}
}
