from pathlib import Path
from typing import Dict
import DAO


class Server:
    __DataBase: DAO.DataBase
    __read_type: str = 'CSV'

    def __init__(self, *args):
        try:
            self.__read_type = Path('lab.property').read_text()
        except FileNotFoundError:
            print('"lab.property" not found')

        if self.__read_type not in ('CSV', 'SQL'):
            print('In "lab.property" should be "CSV" or "SQL"')
            exit()

        if self.__read_type == 'CSV':
            self.__DataBase = DAO.CSVDataBase(*args)    # shops first, products - second
        else:
            self.__DataBase = DAO.SQLDataBase(*args)    # sql server info to connect

    def shop_by_id(self, ind: int) -> str:
        if type(ind) != int:
            print(f'ID type should be int, but found {type(ind)}')
            return 'not a shop'
        return self.__DataBase._shops[ind]

    def id_by_shop(self, name: str) -> int:
        for key in self.__DataBase._shops:
            if self.__DataBase._shops[key] == name:
                return key
        print(f'Sorry, we found no shops called {name}')
        return -1

    def add_shop(self, name: str):   # 1)
        self.__DataBase._shops[len(self.__DataBase._shops) + 1] = name
        self.__DataBase.write_shops(len(self.__DataBase._shops))
        print(f'Shop "{name}" now have ID={len(self.__DataBase._shops)}')

    def add_prod(self, name: str):   # 2)
        if name in self.__DataBase._prods:
            print(f'Product "{name}" already exist')
            return
        self.__DataBase._prods[name] = {}
        self.__DataBase.write_prods(name)
        print(f'Product "{name}" created successfully')

    def prods_to_shop(self, prod_name: str, shop_id: int, amount: int, price: float):   # 3)
        # if you want to change price, just give 0 amount to function

        if prod_name not in self.__DataBase._prods:
            print(f'Product "{prod_name}" not found')
            return
        if shop_id not in self.__DataBase._shops:
            print(f'Shop with ID={shop_id} not found')
            return
        try:
            new_supply = (int(amount), float(price))
            if shop_id in self.__DataBase._prods[prod_name]:
                old_amount = self.__DataBase._prods[prod_name][shop_id][0]
                new_supply = (amount + old_amount, price)
                self.__DataBase._prods[prod_name][shop_id] = new_supply
            else:
                self.__DataBase._prods[prod_name][int(shop_id)] = new_supply
            self.__DataBase.write()
        except (TypeError, ValueError):
            print('Arguments except product name cannot be string')

    def find_cheapest(self, prod_name: str):    # 4) ID, Name, Price
        if prod_name not in self.__DataBase._prods:
            print(f'Product "{prod_name}" not found')
            return
        min_id: int = -1
        min_price: float = -1
        for x in self.__DataBase._prods[prod_name]:
            if min_price == -1 or min_price > self.__DataBase._prods[prod_name][x][1]:
                min_id = x
                min_price = self.__DataBase._prods[prod_name][x][1]
        if min_id not in self.__DataBase._shops:
            print(f'Nobody sales "{prod_name}" product...')
            return
        print(f'Cheapest place to buy "{prod_name}" is {min_id}-{self.shop_by_id(min_id)}, price: {min_price}')

    def what_can_buy(self, shop_id: int, money: float):     # 5)
        if shop_id not in self.__DataBase._shops:
            print(f'Shop with ID={shop_id} not found')
            return
        if money < 0:
            print(f'{money} is not enough to buy anything')
            return
        products: Dict[str, int] = {}
        for prod in self.__DataBase._prods:
            if shop_id in self.__DataBase._prods[prod]:
                if money > self.__DataBase._prods[prod][shop_id][1]:
                    amount = int(money / self.__DataBase._prods[prod][shop_id][1])
                    products[prod] = min(amount, self.__DataBase._prods[prod][shop_id][0])
        if len(products) == 0:
            print(f'{money} is not enough to buy anything')
            return
        print(f'With {money} amount of money you can buy in "{self.shop_by_id(shop_id)}":')
        for prod in products:
            print(f'{products[prod]} of "{prod}"')

    # You can add buying method, using prods_to_shop with amount=old_amount-amount_needed or with new ways
    def buy_lot(self, lot: Dict[str, int], shop_id: int) -> float:     # 6) dictionary of pairs name and amount, shop ID
        if shop_id not in self.__DataBase._shops:
            print(f'"{self.shop_by_id(shop_id)}" do not exist')
            return -1
        price: float = 0.0
        for prod in lot:
            if prod not in self.__DataBase._prods:
                print(f'Product "{prod}" do not exist')
                return -1
            elif shop_id not in self.__DataBase._prods[prod]:
                print(f'"{self.shop_by_id(shop_id)}" do not have product "{prod}"')
                return -1
            elif lot[prod] > self.__DataBase._prods[prod][shop_id][0]:
                print(f'Not enough items "{prod}" to buy')
                return -1
            else:
                price += self.__DataBase._prods[prod][shop_id][1] * lot[prod]
        return price

    def cheapest_lot(self, lot: Dict[str, int]):    # 7)
        min_price: float = 10**10
        min_shop_id: int = -1
        for shop_id in self.__DataBase._shops:
            price: float = 0.0
            for prod in lot:
                if (prod in self.__DataBase._prods) and (shop_id in self.__DataBase._prods[prod]) \
                        and (lot[prod] <= self.__DataBase._prods[prod][shop_id][0]):
                    price += self.__DataBase._prods[prod][shop_id][1] * lot[prod]
                else:
                    price = 10 ** 10 + 1
                    break
            if price < min_price:
                min_price = price
                min_shop_id = shop_id
        if min_shop_id != -1:
            print(f'Cheapest place to buy your lot is "{self.shop_by_id(min_shop_id)}" and it will cost {min_price}')
        else:
            print('There is no place to buy your lot')


'''
Написать методы для следующих операций:
1) Создать магазин
2) Создать товар
3) Завезти партию товаров в магазин (набор товар-количество с возможностью
установить/изменить цену)
4) Найти магазин, в котором определенный товар самый дешевый
5) Понять, какие товары можно купить в магазине на некоторую сумму (например, на 100 рублей можно купить три мороженки 
или две шоколадки)
6) Купить партию товаров в магазине (параметры - сколько каких товаров купить, метод возвращает общую стоимость покупки
либо её невозможность, если товара не хватает)
7) Найти, в каком магазине партия товаров (набор товар-количество) имеет
наименьшую сумму (в целом). Например, «в каком магазине дешевле всего купить 10 гвоздей и 20 шурупов». Наличие товара в
магазинах учитывается!
(во всех пунктах «магазин» — это конкретный магазин, код или название которого передается в параметрах)
'''
