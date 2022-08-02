from typing import List

from client import Client
from account import ACCOUNTS_TYPES, Credit, Debit


class Bank:
    def __init__(self, debit_percentage: float = 3.0,
                 credit_fee: float = 25.0,
                 credit_limit: float = 250000.0,
                 untrusted_limit: float = 100000.0):
        self.clients: List[Client] = []
        self.accounts: List[ACCOUNTS_TYPES] = []
        self.services = {
            Debit: debit_percentage,
            Credit: credit_fee,
        }
        self.untrusted_limit: float = untrusted_limit
        self.credit_limit: float = credit_limit
