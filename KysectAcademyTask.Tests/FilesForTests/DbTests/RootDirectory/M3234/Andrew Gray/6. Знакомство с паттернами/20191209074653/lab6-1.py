import filters
from products import Product, ProductType


def main():
    a = Product(1000, 'red', (3, 5, 10), ProductType.Office)
    a.print_product()
    print('hmm')
    lst = [a, a, a, Product(500, 'yellow', (200, 500, 400), ProductType.Household)]
    filters.print_data(lst)
    print('hmm')
    f = filters.Filter(lst)
    req = 'cost V 0 size V 0'
    f.make_request(req)


if __name__ == '__main__':
    main()

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
