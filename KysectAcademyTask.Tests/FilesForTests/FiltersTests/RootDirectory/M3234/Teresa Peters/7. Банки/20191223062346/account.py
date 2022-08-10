import time

from utils import gen_id


class Debit:
    def __init__(self, bank, client, balance: float = 0.0):
        self.id = gen_id()
        self.bank = bank
        self.client = client
        self._balance: float = balance
        self._delta: float = 0.0

    @property
    def balance(self) -> float:
        return self._balance

    @property
    def delta(self) -> float:
        return self._delta

    def calc_services(self) -> None:
        self._delta += self._balance * (
                self.bank.services[self.__class__] / 365.0)

    def update_services(self) -> None:
        self._balance += self._delta
        self._delta = 0.0

    def __add__(self, other: float) -> 'Debit':
        self._balance += abs(other)
        return self

    def __sub__(self, other: float) -> 'Debit':
        if self._balance < abs(other):
            raise ValueError('Insufficient funds')
        if self.client.unknown and other > self.bank.untrusted_limit:
            raise PermissionError('Client reached untrusted limit')
        self._balance -= abs(other)
        return self


class Credit(Debit):
    def calc_services(self) -> None:
        if self._balance < 0.0:
            self._delta -= self.bank.services[self.__class__]

    def __sub__(self, other) -> 'Credit':
        if self._balance + self.bank.credit_limit < abs(other):
            raise ValueError('Insufficient funds')
        if self.client.unknown and other > self.bank.untrusted_limit:
            raise PermissionError('Client reached untrusted limit')
        self._balance -= abs(other)
        return self


class Deposit(Debit):
    def __init__(self, period: float, *args, **kwargs):
        super().__init__(*args, **kwargs)
        if self._balance < 50000.0:
            self.percentage = 5.0
        elif self._balance < 100000.0:
            self.percentage = 5.5
        else:
            self.percentage = 6.0
        self.creation_time = time.time()
        self.period: float = period

    def calc_services(self) -> None:
        if self.creation_time + self.period < time.time():
            self._delta += self._balance * self.percentage

    def __sub__(self, other) -> 'Deposit':
        if self.creation_time + self.period > time.time():
            raise PermissionError('Deposit period is not reached.')
        # noinspection PyMethodFirstArgAssignment
        self = super().__sub__(other)
        # noinspection PyTypeChecker
        return self


ACCOUNTS_TYPES = [Debit, Credit, Deposit]
