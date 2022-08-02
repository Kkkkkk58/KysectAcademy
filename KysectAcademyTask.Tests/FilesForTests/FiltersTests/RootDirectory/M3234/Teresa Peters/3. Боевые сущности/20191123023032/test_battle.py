import mock

from tests.heroes.battle.fixtures import battle
from tests.heroes.common.fixtures import army, unit_stack


__fixtures__ = [battle, army, unit_stack]


def test_winner(battle):
    """Tests winner method."""
    army = mock.MagicMock()
    army.is_alive = False
    assert battle.winner is None
    with mock.patch.object(battle, 'f_army', army):
        assert battle.winner == battle.s_army
    with mock.patch.object(battle, 's_army', army):
        assert battle.winner == battle.f_army
        with mock.patch.object(battle, 'f_army', army):
            assert battle.winner is None


def test_loser(battle):
    """Tests loser method."""
    assert battle.loser is None
    with mock.patch(
            'meopasl.heroes.battle.Battle.winner',
            new_callable=mock.PropertyMock
    ) as winner_mock:
        winner_mock.return_value = battle.f_army
        assert battle.loser == battle.s_army
    with mock.patch(
            'meopasl.heroes.battle.Battle.winner',
            new_callable=mock.PropertyMock
    ) as winner_mock:
        winner_mock.return_value = battle.s_army
        assert battle.loser == battle.f_army


# TODO: Rewrite this dummy test.
def test_start_battle(battle):
    battle.start_battle()
    assert not battle.f_army.is_alive or not battle.s_army.is_alive
