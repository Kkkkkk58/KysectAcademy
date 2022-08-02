from functools import wraps
from typing import List, Callable, Optional

from meopasl.heroes.battle.battle_units_stack import BattleUnitsStack


class BattleQueue:
    """Battle Queue, which processes moves priority."""

    class _Decorators:
        @staticmethod
        def sort(f):
            """Decorator which sorts BattleQueue by lead stat of units."""
            @wraps(f)
            def wrapper(*args, **kwargs):
                args[0]: BattleQueue
                cmp: Callable[[BattleUnitsStack], int] = \
                    lambda stack: stack.unit.lead
                args[0].queue.sort(key=cmp, reverse=True)
                return f(*args, **kwargs)
            return wrapper

    def __init__(self, stacks: List[BattleUnitsStack]):
        self.queue: List[BattleUnitsStack] = stacks.copy()

    @_Decorators.sort
    def get_next_stack(self) -> Optional[BattleUnitsStack]:
        """Get next stack, which must move.

        :return: Stack or None, if all stacks in Army moved.
        """
        for stack in self.queue:
            if not stack.used:
                return stack
        return None

    @property
    def can_move(self) -> bool:
        """Is there any stack, that isn't used."""
        return any(not stack.used for stack in self.queue)

    @property
    def is_alive(self) -> bool:
        """Is there any stack, that alive."""
        return any(stack.is_alive for stack in self.queue)
