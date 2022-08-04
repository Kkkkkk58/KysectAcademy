from typing import List
from enum import Enum


class DataStorageType(Enum):
    HDD = 0
    SSD = 1


class RamType(Enum):
    DDR3 = 0
    DDR4 = 1


class CoolerType(Enum):
    Tower = 0
    Fan = 1
    Liquid = 2


class Component:
    producer: str = 'none'
    model: str = 'none'

    def __init__(self, producer: str, model: str):
        self.producer, self.model = producer, model


class CPU(Component):
    clock_rate: float

    def __init__(self, producer: str, model: str, cr: float):
        Component.__init__(self, producer, model)
        self.clock_rate = cr


class Data(Component):
    data_type: DataStorageType
    capacity: int

    def __init__(self, producer: str, model: str, data_type: DataStorageType, capacity: int):
        Component.__init__(self, producer, model)
        self.data_type, self.capacity = data_type, capacity


class RAM(Component):
    data_type: RamType
    capacity: int

    def __init__(self, producer: str, model: str, ram_type: RamType, capacity: int):
        Component.__init__(self, producer, model)
        self.data_type, self.capacity = ram_type, capacity


class Cooler(Component):
    cooler_type: CoolerType

    def __init__(self, producer: str, model: str, cooler_type: CoolerType):
        Component.__init__(self, producer, model)
        self.cooler_type = cooler_type


class PortableComputer:
    motherboard: Component = None
    cpu: CPU = None
    data: List[Data] = []
    ram: List[RAM] = []
    gpu: List[Component] = []
    cooling: List[Cooler] = []

    def __init__(self):
        self.motherboard = None
        self.cpu = None
        self.data = []
        self.ram = []
        self.gpu = []
        self.cooling = []


class PCManual:
    _parts: List[str] = []

    def __init__(self):
        self._parts = []

    def print_manual(self) -> None:
        print('Your PC has:')
        for com in self._parts:
            print(com)
