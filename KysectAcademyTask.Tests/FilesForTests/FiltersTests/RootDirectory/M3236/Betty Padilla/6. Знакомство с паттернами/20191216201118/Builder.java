package service;

import entity.*;

interface Builder
{
	void reset();
	void setMotherboard(Motherboard motherboard);
	void setCPU(CPU cpu);
	void setStorage(Storage storage);
	void setRam(RAM[] ram);
	void setGPU(GPU gpu);
	void setLiquidCooling(LiquidCooling lc);
}
