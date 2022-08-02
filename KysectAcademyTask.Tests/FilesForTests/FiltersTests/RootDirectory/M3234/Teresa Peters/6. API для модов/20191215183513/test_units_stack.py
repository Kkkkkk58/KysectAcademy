import pytest

from meopasl.heroes.common.units_stack import UnitsStack
from meopasl.heroes.units.unit import Griffin


@pytest.mark.parametrize('cnt', (
        UnitsStack.MAX_COUNT, UnitsStack.MAX_COUNT + 1
))
def test_units_stack_max_cnt(cnt):
    """Test max limit of units in stack."""
    if cnt > UnitsStack.MAX_COUNT:
        with pytest.raises(UnitsStack.MaxCountUnitsException):
            UnitsStack(Griffin(), cnt)
    else:
        UnitsStack(Griffin(), cnt)


# noinspection PyUnusedLocal
def test_units_stack_modify():
    """Test encapsulation."""
    units_stack = UnitsStack(Griffin(), 5)
    unit = units_stack.unit
    unit = 'something'
    assert isinstance(units_stack.unit, Griffin)
    cnt = units_stack.count
    cnt += 1
    assert units_stack.count == 5
