from random import randint
from typing import Optional

from meopasl.heroes.common.army import Army
from meopasl.heroes.battle.battle_army import BattleArmy


class Battle:
    """Main battle object."""
    def __init__(self, f_army: Army, s_army: Army):
        self.f_army: BattleArmy = BattleArmy(f_army)
        self.s_army: BattleArmy = BattleArmy(s_army)
        self.ended: bool = False

    def start_battle(self):
        cur_army = self.f_army if randint(0, 1) else self.s_army
        nxt_army = self.f_army if cur_army == self.s_army else self.s_army
        while cur_army.is_alive and nxt_army.is_alive:
            cur_army.make_move(nxt_army)
            cur_army, nxt_army = nxt_army, cur_army
        self.ended = True

    @property
    def winner(self) -> Optional[BattleArmy]:
        """Return winner's Army or None, if suck not exists."""
        if self.f_army.is_alive == self.s_army.is_alive:
            return None
        return self.f_army if self.f_army.is_alive else self.s_army

    @property
    def loser(self) -> Optional[BattleArmy]:
        """Return loser's Army or None, if suck not exists."""
        if self.winner is None:
            return None
        return self.f_army if self.winner == self.s_army else self.s_army
