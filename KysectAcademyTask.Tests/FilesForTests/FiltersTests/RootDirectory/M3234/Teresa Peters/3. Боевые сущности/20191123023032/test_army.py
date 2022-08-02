import pytest
from copy import copy

from meopasl.heroes.common.army import Army
from meopasl.heroes.common.units_stack import UnitsStack
from tests.heroes.common.fixtures import unit_stack, army


__fixtures__ = [unit_stack, army]


@pytest.mark.parametrize('cnt', (
        Army.MAX_COUNT, Army.MAX_COUNT + 1
))
def test_army_max_cnt(cnt, unit_stack):
    """Test max limit of stacks in army."""
    if cnt > Army.MAX_COUNT:
        with pytest.raises(Army.MaxCountUnitsException):
            Army([unit_stack] * cnt)
    else:
        army = Army([unit_stack] * cnt)
        with pytest.raises(Army.MaxCountUnitsException):
            army.add_stack(unit_stack)


def test_army_stack_modify(army, unit_stack):
    """Test encapsulation on append."""
    stacks = army.stacks
    stacks.append(unit_stack)
    assert len(army) == 3


def test_army_add_stack(army, unit_stack):
    """Test add_stack method."""
    army.add_stack(unit_stack)
    assert len(army) == 4


def test_army_remove_stack(army, unit_stack):
    """Test remove and __delitem__ methods."""
    army.remove_stack(unit_stack)
    assert len(army) == 2
    del army[0]
    assert len(army) == 1
    with pytest.raises(Army.StackNotFound):
        army.remove_stack(copy(unit_stack))
    with pytest.raises(Army.IndexError):
        del army[123]


def test_army_contains(army, unit_stack):
    """Test __contains__ method."""
    assert unit_stack in army
    assert army.stacks[0] in army
    assert copy(unit_stack) not in army


def test_army_iter(army):
    """Test __iter__ method."""
    for stack in army:
        assert isinstance(stack, UnitsStack)


# noinspection PyStatementEffect
def test_army_get_item(army, unit_stack):
    """Test __getitem__ method."""
    assert army[1] == unit_stack
    with pytest.raises(Army.IndexError):
        army[123]


def test_army_length(army):
    assert len(army) == 3
