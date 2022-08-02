package entity;

public class Client
{
	String name;
	String address;
	Long passportID;

	Client(String name, String address, Long passportID) {
		this.name = name;
		this.address = address;
		this.passportID = passportID;
	}
}
