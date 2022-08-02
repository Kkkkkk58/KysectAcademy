from copy import copy

from meopasl.heroes.units.unit import units_type


class UnitsStack:
    """Stack of units."""
    MAX_COUNT = 999999

    class MaxCountUnitsException(Exception):
        """Exception matters, that max count of units reached."""
        def __init__(self):
            super().__init__("Max count of units in one stack is {}"
                             .format(UnitsStack.MAX_COUNT))

    def __init__(self, unit: units_type, count: int):
        """
        :param unit: Type of unit.
        :param count: Count of units in stack.
        """
        self._unit: units_type = unit
        if count > self.MAX_COUNT:
            raise self.MaxCountUnitsException()
        self._count: int = count

    @property
    def unit(self) -> units_type:
        """Getter for unit type"""
        return copy(self._unit)

    @property
    def count(self) -> int:
        """Getter for count of units"""
        return self._count

    def __repr__(self) -> str:
        """Represent stack of units to human-presentable view."""
        return ('<UnitsStack with {} of {}>'
                .format(self._count, repr(self._unit)))
