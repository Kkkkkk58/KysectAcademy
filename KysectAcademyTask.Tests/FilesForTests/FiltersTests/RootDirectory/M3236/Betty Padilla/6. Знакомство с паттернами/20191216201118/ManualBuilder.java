package service;

import entity.*;

import java.util.Arrays;

public class ManualBuilder implements Builder
{
	private ComputerManual manual;

	@Override
	public void reset() {
		manual = new ComputerManual();
	}

	@Override
	public void setMotherboard(Motherboard motherboard) {
		manual.add(motherboard.toString());
	}

	@Override
	public void setCPU(CPU cpu) {
		manual.add(cpu.toString());
	}

	@Override
	public void setStorage(Storage storage) {
		manual.add(storage.toString());
	}

	@Override
	public void setRam(RAM[] ram) {
		manual.add(Arrays.toString(ram));
	}

	@Override
	public void setGPU(GPU gpu) {
		manual.add(gpu.toString());
	}

	@Override
	public void setLiquidCooling(LiquidCooling lc) {
		manual.add(lc.toString());
	}

	public ComputerManual getResult()
	{
		return manual;
	}
}
