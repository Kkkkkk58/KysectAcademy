from typing import Dict, Tuple
from pathlib import Path


class DataBase:
    _shops: Dict[int, str] = {}
    _prods: Dict[str, Dict[int, Tuple[int, float]]] = {}

    def read(self):
        pass

    def write(self):
        pass

    def write_shops(self, *args):
        pass

    def write_prods(self, *args):
        pass


class SQLDataBase(DataBase):    # затычка
    _SQL_path: str = None

    def __init__(self, *args):  # init is not done yet
        if len(args) == 1:
            self._SQL_path = args[0]
            '''
            There should be trying to open sql db
            '''
        elif len(args) > 1:
            print('Too much arguments for initialization')
        else:
            print('Not enough arguments for initialization')

    def read(self):
        pass

    def write(self):
        pass

    def write_shops(self, *args):
        pass

    def write_prods(self, *args):
        pass


class CSVDataBase(DataBase):
    _CSV_shop: str = None
    _CSV_prod: str = None

    def __init__(self, *args):
        if len(args) == 2:
            self._CSV_shop = args[0]
            try:
                Path(self._CSV_shop).read_text()
            except FileNotFoundError:
                print('"{}" not found'.format(self._CSV_shop))
            self._CSV_prod = args[1]
            try:
                Path(self._CSV_prod).read_text()
            except FileNotFoundError:
                print('"{}" not found'.format(self._CSV_prod))
        elif len(args) > 2:
            print('Too much arguments for initialization')
            exit(1)
        else:
            print('Not enough arguments for initialization')
            exit(1)
        self._shops = self.read_shops()
        self._prods = self.read_prods()

    def read_prods(self) -> Dict[str, Dict[int, Tuple[int, float]]]:
        d: Dict[str, Dict[int, Tuple[int, float]]] = {}
        prods = Path(self._CSV_prod).read_text()
        try:
            for lines in prods.splitlines():
                line = lines.split(',')
                name: str = line[0]
                d[name]: Dict[int, Tuple[int, float]] = {}
                if len(line) != 2 or line[1] != '':
                    for i in range(1, len(line), 3):
                        try:
                            d[name][int(line[i])] = int(line[i + 1]), float(line[i + 2])
                        except (TypeError, ValueError):
                            print(f'Cannot convert string to float in Product "{name}" line')
                            exit(1)
        except IndexError:
            print(f'CSVFormatError: error while reading "{self._CSV_prod}", wrong CSV format found')
            exit(1)
        return d

    def read_shops(self) -> Dict[int, str]:
        d: Dict[int, str] = {}
        shops = Path(self._CSV_shop).read_text()
        try:
            for lines in shops.splitlines():
                line = lines.split(',')
                d[int(line[0])] = line[1]
        except (IndexError, TypeError):
            print(f'CSVFormatError: error while reading "{self._CSV_shop}", wrong CSV format found')
            exit(1)
        return d

    def read(self) -> Tuple[Dict[int, str], Dict[str, Dict[int, Tuple[int, float]]]]:
        return self._shops, self._prods

    def write(self):
        t = open(self._CSV_prod, 'w')
        for prod in self._prods:
            t.write(prod)
            for info in self._prods[prod]:
                t.write(',' + str(info) + ',' + str(self._prods[prod][info][0]) + ',' + str(self._prods[prod][info][1]))
            t.write('\n')

    def write_shops(self, shop):
        t = open(self._CSV_shop, 'a')
        t.write(str(shop) + ',' + self._shops[shop] + '\n')

    def write_prods(self, prod):
        t = open(self._CSV_prod, 'a')
        t.write(prod + ',\n')
