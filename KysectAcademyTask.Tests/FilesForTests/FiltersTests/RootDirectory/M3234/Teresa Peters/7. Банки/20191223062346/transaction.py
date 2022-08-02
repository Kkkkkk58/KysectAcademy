from utils import gen_id
from account import ACCOUNTS_TYPES


class Transaction:
    def __init__(self, first: ACCOUNTS_TYPES, second: ACCOUNTS_TYPES,
                 amount: float):
        self.id = gen_id()
        self.first: ACCOUNTS_TYPES = first
        self.second: ACCOUNTS_TYPES = second
        self.amount: float = amount
        self.applied: bool = False

    def apply(self) -> None:
        if self.applied:
            raise PermissionError('Transaction already applied.')
        self.first -= self.amount
        self.second += self.amount
        self.applied = True

    def rollback(self) -> None:
        if not self.applied:
            raise PermissionError('Transaction not applied.')
        self.first += self.amount
        self.second -= self.amount
        self.applied = False
