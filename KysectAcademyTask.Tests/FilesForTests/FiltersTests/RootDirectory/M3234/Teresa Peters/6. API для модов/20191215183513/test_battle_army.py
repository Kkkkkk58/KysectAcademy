import mock
from copy import deepcopy

from tests.heroes.battle.fixtures import battle_army
from tests.heroes.common.fixtures import army, unit_stack


__fixtures__ = [battle_army, army, unit_stack]


# TODO: Rewrite this dummy test
def test_make_move(battle_army):
    """Tests make_move method."""
    other_battle_army = deepcopy(battle_army)
    init_count = other_battle_army.queue.queue[0].count
    battle_army.make_move(other_battle_army)
    assert other_battle_army.queue.queue[0].count < init_count


def test_is_alive(battle_army):
    """Tests is_alive method."""
    assert battle_army.is_alive
    queue = mock.MagicMock()
    queue.is_alive = False
    with mock.patch.object(battle_army, 'queue', queue):
        assert not battle_army.is_alive
