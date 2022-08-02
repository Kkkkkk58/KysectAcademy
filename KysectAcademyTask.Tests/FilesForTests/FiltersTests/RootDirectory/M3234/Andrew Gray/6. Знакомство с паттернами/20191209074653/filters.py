from products import Product
from typing import List, Union, Deque, Tuple


def cost_sort(product: Product) -> Union[float, int]:
    return product.cost


def color_sort(product: Product) -> str:
    return product.color


def size_sort(product: Product) -> float:
    return product.size[0] * product.size[1] * product.size[2]


def type_sort(product: Product) -> int:
    return product.prod_type.value


def filter_decor(func, name, rtype: str = None):    # value: float = None
    def wrapper(data: List[Product] = None):
        if rtype in ['^', 'V']:
            data.sort(key=name, reverse=(True if rtype == 'V' else False))
        func(data)
    return wrapper


def print_data(data: List[Product]) -> None:
    for prod in data:
        prod.print_product()


class Filter:
    data: List[Product] = []

    def __init__(self, data: List[Product]):
        self.data = data

    def make_request(self, request: str):
        stack: Deque[Tuple[str, str, float]] = []
        result: function = print_data
        t = request.split()
        for i in range(0, len(t), 3):
            stack.append((t[i], t[i + 1], float(t[i + 2])))

        while len(stack) > 0:
            t = stack.pop()
            tmp: function = cost_sort
            if t[0].lower() == 'cost':
                tmp = cost_sort
            elif t[0].lower() == 'color':
                tmp = color_sort
            elif t[0].lower() == 'size':
                tmp = size_sort
            elif t[0].lower() == 'type':
                tmp = type_sort
            else:
                print('something wrong with column name, i will sort by cost')
            result = filter_decor(result, tmp, t[1])  # t[2]

        result(self.data)


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
