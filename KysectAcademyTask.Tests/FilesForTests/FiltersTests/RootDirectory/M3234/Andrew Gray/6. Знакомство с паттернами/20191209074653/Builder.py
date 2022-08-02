import PC
from PC import RamType
from PC import DataStorageType
from PC import CoolerType
from typing import List


class Builder:
    def reset(self) -> None:
        pass

    def set_motherboard(self, motherboard: PC.Component) -> None:
        pass

    def set_cpu(self, cpu: PC.CPU) -> None:
        pass

    def set_data(self, data: PC.Data) -> None:
        pass

    def set_ram(self, ram: PC.RAM) -> None:
        pass

    def set_gpu(self, gpu: PC.Component) -> None:
        pass

    def set_cooler(self, cooler: PC.Cooler) -> None:
        pass


class PCBuilder(Builder):
    _PC: PC.PortableComputer()

    def __init__(self):
        self.reset()

    def reset(self) -> None:
        self._PC = PC.PortableComputer()

    def set_motherboard(self, motherboard: PC.Component) -> None:
        self._PC.motherboard = motherboard

    def set_cpu(self, cpu: PC.CPU) -> None:
        self._PC.cpu = cpu

    def set_data(self, data: PC.Data) -> None:
        self._PC.data.append(data)

    def set_ram(self, ram: PC.RAM) -> None:
        self._PC.ram.append(ram)

    def set_gpu(self, gpu: PC.Component) -> None:
        self._PC.gpu.append(gpu)

    def set_cooler(self, cooler: PC.Cooler) -> None:
        self._PC.cooling.append(cooler)

    def get_result(self) -> PC.PortableComputer():
        if (self._PC.cpu is not None) and (self._PC.motherboard is not None) and \
                (len(self._PC.data) > 0) and (len(self._PC.ram) > 0):
            return self._PC
        else:
            print('Some stuff in your PC is not ready yet')
            return None


class PCManualBuilder(Builder):
    _PCManual: PC.PCManual()
    __is_ready: List[bool] = [False, False, False, False]

    def __init__(self):
        self.reset()

    def reset(self) -> None:
        self._PCManual = PC.PCManual()
        self.__is_ready = [False, False, False, False]

    def set_motherboard(self, motherboard: PC.Component) -> None:
        self._PCManual._parts.append(f'Motherboard: {motherboard.producer} {motherboard.model}')
        self.__is_ready[0] = True

    def set_cpu(self, cpu: PC.CPU) -> None:
        self._PCManual._parts.append(f'CPU: {cpu.producer} {cpu.model} {cpu.clock_rate} GHz')
        self.__is_ready[1] = True

    def set_data(self, data: PC.Data) -> None:
        self._PCManual._parts.append(f'Data Storage: {data.producer} {data.model} {data.data_type} {data.capacity} GB')
        self.__is_ready[2] = True

    def set_ram(self, ram: PC.RAM) -> None:
        self._PCManual._parts.append(f'RAM: {ram.producer} {ram.model} {ram.data_type} {ram.capacity} GB')
        self.__is_ready[3] = True

    def set_gpu(self, gpu: PC.Component) -> None:
        self._PCManual._parts.append(f'GPU: {gpu.producer} {gpu.model}')

    def set_cooler(self, cooler: PC.Cooler) -> None:
        self._PCManual._parts.append(f'Cooler: {cooler.producer} {cooler.model} {cooler.cooler_type}')

    def get_result(self) -> PC.PCManual():
        if self.__is_ready == [True, True, True, True]:
            return self._PCManual
        else:
            print('Some stuff in your PC is not ready yet')
            return None


class Director:
    _builder: Builder = None

    def __init__(self) -> None:
        self._builder = None

    def get_builder(self) -> Builder:
        return self._builder

    def builder(self, builder: Builder) -> None:
        self._builder = builder

    def economy(self) -> None:
        self._builder.reset()
        self._builder.set_motherboard(PC.Component('ASROCK', 'FM2A68M-DG3'))
        self._builder.set_cpu(PC.CPU('AMD', 'A4 4000', 3.0))
        self._builder.set_ram(PC.RAM('PATRIOT', 'PSD32G16002', RamType.DDR3, 2))
        self._builder.set_data(PC.Data('HGST', 'Travelstar Z5K500', DataStorageType.HDD, 500))

    def middle(self) -> None:
        pass

    def comfort(self) -> None:
        pass

    def top(self) -> None:
        self._builder.reset()
        self._builder.set_motherboard(PC.Component('Acer', 'Predator'))
        self._builder.set_cpu(PC.CPU('Intel', 'Core i9 7980XE', 3.3))
        self._builder.set_ram(PC.RAM('Acer', '', RamType.DDR4, 64))
        self._builder.set_data(PC.Data('WD', 'Red [WD30EFRX]', DataStorageType.HDD, 3000))
        self._builder.set_data(PC.Data('INTEL', 'Optane 905P SSDPED1D960GAX1', DataStorageType.SSD, 960))
        self._builder.set_gpu(PC.Component('NVIDIA', 'GeForce RTX 2080Ti'))
        self._builder.set_gpu(PC.Component('NVIDIA', 'GeForce RTX 2080Ti'))
        self._builder.set_cooler(PC.Cooler('Asus', 'ROG RYUJIN 360 90RC0020-M0UAY0', CoolerType.Liquid))
        self._builder.set_cooler(PC.Cooler('COOLER MASTER', 'MAM-D7PN-DWRPS-T1', CoolerType.Tower))
