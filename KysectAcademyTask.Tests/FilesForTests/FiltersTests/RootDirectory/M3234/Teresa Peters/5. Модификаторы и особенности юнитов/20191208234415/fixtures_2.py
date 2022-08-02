import pytest

from meopasl.heroes.common.army import Army
from meopasl.heroes.common.units_stack import UnitsStack
from meopasl.heroes.units.unit import Griffin


@pytest.fixture(scope='function')
def unit_stack() -> UnitsStack:
    """Get UnitStack object."""
    return UnitsStack(Griffin(), 123)


@pytest.fixture(scope='function')
def army(unit_stack) -> Army:
    """Get army object."""
    return Army([unit_stack] * 3)
