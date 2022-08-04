package entity;

public class ComputerManual
{
	private String text = "";

	@Override
	public String toString() {
		return text;
	}

	public void add(String s)
	{
		text += s + "\n";
	}
}
