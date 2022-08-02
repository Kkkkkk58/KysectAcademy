package service;

import entity.*;

public class ComputerBuilder implements Builder
{
	private Computer computer;
	@Override
	public void reset() {
		computer = new Computer();
	}

	@Override
	public void setMotherboard(Motherboard motherboard) {
		computer.motherboard = motherboard;
	}

	@Override
	public void setCPU(CPU cpu) {
		computer.cpu = cpu;
	}

	@Override
	public void setStorage(Storage storage) {
		computer.storage = storage;
	}

	@Override
	public void setRam(RAM[] ram) {
		computer.ram = ram;
	}

	@Override
	public void setGPU(GPU gpu) {
		computer.gpu = gpu;
	}

	@Override
	public void setLiquidCooling(LiquidCooling lc) {
		computer.liquidCooling = lc;
	}

	public Computer getResult() {
		return computer;
	}
}
