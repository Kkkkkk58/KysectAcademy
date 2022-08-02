from meopasl.heroes.common.army import Army
from meopasl.heroes.battle.battle_units_stack import BattleUnitsStack
from meopasl.heroes.battle.battle_queue import BattleQueue


# TODO: Implement normal interface.
# noinspection PyUnusedLocal
def some_interface(actions, other: 'BattleArmy'):
    for stack in other.queue.queue:
        if stack.is_alive:
            return BattleUnitsStack.Actions.ATTACK, stack


class BattleArmy:
    """Implements battle version of Army."""
    def __init__(self, army: Army):
        stacks = [BattleUnitsStack(stack) for stack in army]
        self.queue: BattleQueue = BattleQueue(stacks)

    @property
    def is_alive(self) -> bool:
        """Is army alive."""
        return self.queue.is_alive

    def make_move(self, other: 'BattleArmy') -> None:
        """Make complete move of whole army."""
        while self.queue.can_move:
            stack: BattleUnitsStack = self.queue.get_next_stack()
            stack.make_move(*some_interface(stack.actions, other))
        for stack in self.queue.queue:
            stack.end_move()
