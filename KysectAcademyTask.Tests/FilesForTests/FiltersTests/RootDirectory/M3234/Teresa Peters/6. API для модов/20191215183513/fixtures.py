import pytest
from copy import deepcopy
from typing import Tuple

from meopasl.heroes.battle import Battle
from meopasl.heroes.battle.battle_units_stack import BattleUnitsStack
from meopasl.heroes.battle.battle_army import BattleArmy
from meopasl.heroes.battle.battle_queue import BattleQueue
from tests.heroes.common.fixtures import unit_stack, army


__fixtures__ = [unit_stack, army]


@pytest.fixture(scope='function')
def battle_unit_stack(unit_stack) -> BattleUnitsStack:
    return BattleUnitsStack(unit_stack)


@pytest.fixture(scope='function')
def battle_unit_stacks(
        unit_stack
) -> Tuple[BattleUnitsStack, BattleUnitsStack]:
    return BattleUnitsStack(unit_stack), BattleUnitsStack(unit_stack)


@pytest.fixture(scope='function')
def battle_army(army) -> BattleArmy:
    return BattleArmy(army)


@pytest.fixture(scope='function')
def battle_queue(battle_unit_stacks) -> BattleQueue:
    battle_unit_stacks[1].unit.lead -= 2
    return BattleQueue([stack for stack in battle_unit_stacks])


@pytest.fixture(scope='function')
def battle(army) -> Battle:
    return Battle(army, deepcopy(army))
