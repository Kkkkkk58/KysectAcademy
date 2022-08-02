from tests.heroes.battle.fixtures import battle_queue, battle_unit_stacks
from tests.heroes.common.fixtures import unit_stack

__fixtures__ = [battle_queue, battle_unit_stacks, unit_stack]


def test_get_next_stack(battle_queue):
    """Tests get_next_stack method."""
    if battle_queue.queue[0].unit.lead > battle_queue.queue[1].unit.lead:
        battle_queue.queue.reverse()
    f = battle_queue.get_next_stack()
    f.wait()
    s = battle_queue.get_next_stack()
    assert abs(f.unit.lead) > s.unit.lead
    s._used = True
    assert f == battle_queue.get_next_stack()
    f._used = True
    assert battle_queue.get_next_stack() is None


def test_can_move(battle_queue):
    """Tests can_move method."""
    assert battle_queue.can_move
    battle_queue.queue[0]._used = True
    assert battle_queue.can_move
    battle_queue.queue[1]._used = True
    assert not battle_queue.can_move


def test_is_alive(battle_queue):
    """Tests is_alive method."""
    assert battle_queue.is_alive
    battle_queue.queue[0].count = 0
    assert battle_queue.is_alive
    battle_queue.queue[1].count = 0
    assert not battle_queue.is_alive
