package service;

import entity.*;

public class Director extends Components
{
	public enum Configuration {
		Budget,
		Mediocre,
		Comfort,
		Highend
	}


	public void setConfig(Configuration config)
	{
		switch (config)
		{
			case Budget:
				cpu = new CPU("Celeron", "Intel", 1.33d);
				motherboard = new Motherboard("ABC", "ASUS");
				storage = new Storage(Storage.Type.HHD, "qwe", "Sun");
				ram[0] = new RAM(RAM.Type.DDR3, "qwa", "Kingston", 1);
				ram[1] = null;
				ram[2] = null;
				ram[3] = null;
				gpu = null;
				liquidCooling = null;
				break;
			case Mediocre:
				cpu = new CPU("Pentium", "Intel", 1.5d);
				motherboard = new Motherboard("ABC", "ASUS");
				storage = new Storage(Storage.Type.HHD, "ST44", "Seagate");
				ram[0] = new RAM(RAM.Type.DDR3, "cc2", "Crucial", 2);
				ram[1] = null;
				ram[2] = new RAM(RAM.Type.DDR3, "cc2", "Crucial", 2);
				ram[3] = null;
				gpu = null;
				liquidCooling = null;
				break;
			case Comfort:
				cpu = new CPU("i-5", "Intel", 3.1d);
				motherboard = new Motherboard("GA423", "Gigabyte");
				storage = new Storage(Storage.Type.HHD, "U44", "Seagate");
				ram[0] = new RAM(RAM.Type.DDR3, "cc4", "Crucial", 4);
				ram[1] = null;
				ram[2] = new RAM(RAM.Type.DDR3, "cc4", "Crucial", 4);
				ram[3] = null;
				gpu = new GPU("gtx 1060", "Nvidia");
				liquidCooling = null;
				break;
			case Highend:
				cpu = new CPU("i-7", "Intel", 3.4d);
				motherboard = new Motherboard("TTR", "HyperX");
				storage = new Storage(Storage.Type.SDD, "R1000", "Kingston");
				ram[0] = new RAM(RAM.Type.DDR4, "q4", "HyperX", 4);
				ram[1] = new RAM(RAM.Type.DDR4, "q4", "HyperX", 4);
				ram[2] = new RAM(RAM.Type.DDR4, "q4", "HyperX", 4);
				ram[3] = new RAM(RAM.Type.DDR4, "q4", "HyperX", 4);
				gpu = new GPU("rtx 2070", "nvidia");
				liquidCooling = new LiquidCooling("LC", "Deepcool");
				break;
		}
	}

	public void make(Builder builder)
	{
		builder.reset();
		builder.setMotherboard(motherboard);
		builder.setCPU(cpu);
		builder.setStorage(storage);
		builder.setRam(ram.clone());

		if (gpu != null)
			builder.setGPU(gpu);

		if (liquidCooling != null)
			builder.setLiquidCooling(liquidCooling);
	}
}
