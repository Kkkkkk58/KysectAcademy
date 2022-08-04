package entity;

public class Storage
{
	public enum Type
	{
		HHD,
		SDD
	}

	Type type;

	String model;
	String vendor;

	public Storage(Type type, String model, String vendor) {
		this.type = type;
		this.model = model;
		this.vendor = vendor;
	}

	@Override
	public String toString() {
		return "Storage{" +
				"type=" + type +
				", model='" + model + '\'' +
				", vendor='" + vendor + '\'' +
				'}';
	}
}
