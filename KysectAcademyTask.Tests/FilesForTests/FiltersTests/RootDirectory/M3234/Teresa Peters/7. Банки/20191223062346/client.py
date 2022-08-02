from typing import Optional


class Client:
    def __init__(self, first_name: str, second_name: str,
                 passport: Optional[str] = None,
                 address: Optional[str] = None):
        self.first_name: str = first_name
        self.second_name: str = second_name
        self.passport: Optional[str] = passport
        self.address: Optional[str] = address

    @property
    def unknown(self) -> bool:
        return self.passport is None or self.address is None
