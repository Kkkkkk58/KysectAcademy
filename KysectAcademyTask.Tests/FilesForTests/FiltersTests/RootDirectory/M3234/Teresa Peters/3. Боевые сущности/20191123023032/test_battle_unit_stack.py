import mock
import pytest

from meopasl.heroes.battle.battle_units_stack import BattleUnitsStack
from tests.heroes.battle.fixtures import battle_unit_stack, battle_unit_stacks
from tests.heroes.common.fixtures import unit_stack


__fixtures__ = [battle_unit_stack, unit_stack, battle_unit_stacks]


@pytest.mark.parametrize(
    "count, alive", [
        (123, True),
        (1, True),
        (0, False),
        (-123, False)
    ]
)
def test_is_alive(battle_unit_stack, count, alive):
    """Tests that init_count is private."""
    battle_unit_stack.count = count
    assert battle_unit_stack.is_alive == alive


def test_wait_and__unwait(battle_unit_stack):
    """Tests wait and _unwait methods."""
    init_lead = battle_unit_stack.unit.lead
    battle_unit_stack.wait()
    assert battle_unit_stack.unit.lead == -init_lead
    assert not battle_unit_stack.actions[BattleUnitsStack.Actions.WAIT]
    assert any(battle_unit_stack.actions.values())
    assert (battle_unit_stack._unwait, (), {}) in battle_unit_stack._end_move
    battle_unit_stack.defend()
    battle_unit_stack.end_move()
    assert battle_unit_stack.unit.lead == init_lead
    assert battle_unit_stack.actions[BattleUnitsStack.Actions.WAIT]
    assert ((battle_unit_stack._unwait, (), {})
            not in battle_unit_stack._end_move)


def test_defend_and__undefend(battle_unit_stack):
    """Tests defend and _undefend methods."""
    init_armor = battle_unit_stack.unit.armor
    battle_unit_stack.defend()
    assert battle_unit_stack.unit.armor == int(init_armor * 1.3)
    assert battle_unit_stack.used
    assert not any(battle_unit_stack.actions.values())
    assert ((battle_unit_stack._undefend, (init_armor,), {})
            in battle_unit_stack._end_move)
    battle_unit_stack.end_move()
    assert battle_unit_stack.unit.armor == init_armor
    assert battle_unit_stack.actions[BattleUnitsStack.Actions.DEFEND]
    assert ((battle_unit_stack._undefend, (init_armor,), {})
            not in battle_unit_stack._end_move)


def test_attack(battle_unit_stacks):
    """Tests attack method."""
    f_stack, s_stack = battle_unit_stacks
    f_stack.attack(s_stack)
    assert f_stack.count != s_stack.count
    assert not f_stack.actions[BattleUnitsStack.Actions.ATTACK]
    f_stack.end_move()
    assert f_stack.actions[BattleUnitsStack.Actions.ATTACK]
    f_stack.unit.damage = (100000, 100000)
    f_stack.attack(s_stack)
    assert not s_stack.is_alive
    assert not f_stack.actions[BattleUnitsStack.Actions.ATTACK]
    f_stack.end_move()
    assert f_stack.actions[BattleUnitsStack.Actions.ATTACK]


def test_make_used_and__unmake_used(battle_unit_stack):
    """Tests make_used decorator and _unmake_used methods."""
    battle_unit_stack.defend()
    assert not any(battle_unit_stack.actions.values())
    assert battle_unit_stack.used
    assert ((battle_unit_stack._unmake_used, (), {})
            in battle_unit_stack._end_move)
    battle_unit_stack.end_move()
    assert not battle_unit_stack.used
    assert all(battle_unit_stack.actions.values())
    assert ((battle_unit_stack._unmake_used, (), {})
            not in battle_unit_stack._end_move)


def test_make_move(battle_unit_stacks):
    """Tests make_move method."""
    f_stack, s_stack = battle_unit_stacks
    for action in BattleUnitsStack.Actions:
        with mock.patch.object(f_stack, action.value) as action_mock:
            f_stack.make_move(action, s_stack)
            f_stack._actions[action] = False
            with pytest.raises(BattleUnitsStack.ActionNotAvailable):
                f_stack.make_move(action)
            action_mock.assert_called_once_with(s_stack)


def test_cast(battle_unit_stack):
    """Test cast method."""
    with mock.patch.object(battle_unit_stack.unit, 'cast') as cast:
        battle_unit_stack.cast(battle_unit_stack)
    cast.assert_called_once_with(
        count=battle_unit_stack.count, unit_stack=battle_unit_stack
    )
