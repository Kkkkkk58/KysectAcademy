from enum import Enum
from typing import Union, Tuple


class ProductType(Enum):
    Household = 0
    Clothes = 1
    Toys = 2
    Accessories = 3
    Office = 4


class Product:
    cost: Union[float, int] = 0
    color: str = 'black'
    size: Tuple[int, int, int] = (0, 0, 0)
    prod_type: ProductType = None

    def __init__(self, cost: Union[float, int], color: str,
                 size: Tuple[int, int, int], prod_type: ProductType) -> None:
        self.cost, self.color, self.size, self.prod_type = cost, color, size, prod_type

    def print_product(self):
        print(f'Cost: {self.cost}; \
                Color: {self.color}; \
                Size: {self.size[0]}x{self.size[1]}x{self.size[2]}; \
                Type: {self.prod_type.name}')


class NewProduct(Product):
    country: str = 'Chine'

    def __init__(self, cost: Union[float, int], color: str,
                 size: Tuple[int, int, int], prod_type: ProductType, country: str) -> None:
        Product.__init__(self, cost, color, size, prod_type)
        self.country = country

    def print_product(self):
        print(f'Cost: {self.cost}; \
                Color: {self.color}; \
                Size: {self.size[0]}x{self.size[1]}x{self.size[2]}; \
                Type: {self.prod_type.name}; \
                Country: {self.country}')


'''
Имеется коллекция товаров (список). Товар имеет набор характеристик: цена, цвет, размер,
тип (перечисление). Пользователи хотят иметь возможность фильтровать список товаров в
зависимости от своих предпочтений. Должны быть фильтры на каждую характеристику. Необходимо
реализовать функционал, позволяющий удовлетворить потребность пользователей в фильтрации.
Следует учесть, что в будущем у товара могут добавляться новые характеристики, возможно,
экзотических типов, на которые тоже должны будут появиться фильтры со своей логикой.
Бонусом будет реализация комбинирования существующих фильтров (как во многих современных
интернет-магазинах)

'''
