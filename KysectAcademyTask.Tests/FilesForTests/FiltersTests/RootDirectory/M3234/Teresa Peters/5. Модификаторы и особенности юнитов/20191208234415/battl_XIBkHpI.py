from enum import Enum
from random import randint
from functools import wraps
from typing import List, Callable, Tuple, Dict, Any, Optional

from meopasl.heroes.common.units_stack import UnitsStack
from meopasl.heroes.units.skills import Skills
from meopasl.heroes.units.unit import units_type


class BattleUnitsStack:
    """Battle stack of units."""
    MAX_COUNT = 999999

    class Actions(Enum):
        ATTACK = 'attack'
        CAST = 'cast'
        DEFEND = 'defend'
        WAIT = 'wait'

    class MaxCountUnitsException(Exception):
        """Exception matters, that max count of units reached."""
        def __init__(self):
            super().__init__("Max count of units in one stack is {}"
                             .format(UnitsStack.MAX_COUNT))

    class ActionNotAvailable(Exception):
        """Exception matters, that some action cannot be used."""
        def __init__(self, action):
            super().__init__("Action {} is unavailable.".format(action))

    class _Decorators:
        @staticmethod
        def make_used(f):
            """Decorator for setting attrs to used state, after func call."""

            # noinspection PyProtectedMember
            @wraps(f)
            def wrapper(*args, **kwargs):
                res = f(*args, **kwargs)
                args[0]._used = True
                args[0]._actions = dict.fromkeys(args[0]._actions.keys(),
                                                 False)
                args[0]._end_move.append((args[0]._unmake_used, (), {}))
                return res
            return wrapper

    def __init__(self, unit_stack: UnitsStack):
        self.unit: units_type = unit_stack.unit
        if unit_stack.count > self.MAX_COUNT:
            raise self.MaxCountUnitsException()
        self.count: int = unit_stack.count
        self._init_count: int = unit_stack.count
        self._used: bool = False
        self._end_move: \
            List[Tuple[Callable, Tuple[Any, ...], Dict[Any, Any]]] = []
        self._actions: Dict[BattleUnitsStack.Actions, bool] = \
            {act: True for act in BattleUnitsStack.Actions}

    def __repr__(self) -> str:
        """Represent battle stack of units to human-presentable view."""
        return ('<BattleUnitsStack with {} of {}>'
                .format(self.count, repr(self.unit)))

    @property
    def init_count(self) -> int:
        """Get initial count of units in stack."""
        return self._init_count

    @property
    def used(self) -> bool:
        """Is stack used in current move."""
        return self._used

    @property
    def is_alive(self) -> bool:
        """Is stack alive."""
        return self.count > 0

    @property
    def actions(self) -> Dict['BattleUnitsStack.Actions', bool]:
        """Returns all available actions for stack."""
        return self._actions.copy()

    def _unmake_used(self):
        """Reset unit's stats after _make_used method."""
        self._used = False
        self._actions = dict.fromkeys(self._actions.keys(), True)

    def end_move(self) -> None:
        """Applies some changes, that needed in the end of every move."""
        for func, args, kwargs in self._end_move:
            func(*args, **kwargs)
        self._end_move.clear()

    def make_move(self, action: 'BattleUnitsStack.Actions',
                  other: Optional['BattleUnitsStack'] = None) -> None:
        """Interface for making moves, declared in Actions."""
        if not self._actions[action]:
            raise BattleUnitsStack.ActionNotAvailable(action)
        self.__getattribute__(action.value)(other)

    @_Decorators.make_used
    def attack(
            self, unit_stack: 'BattleUnitsStack',
            *_args, _fight_back=False
    ) -> None:
        """Attack other battle stack."""
        armor = (0 if Skills.PRECISE_SHOT in self.unit.skills
                 else unit_stack.unit.armor)
        cnt = (self.count * self.unit.damage[0],
               self.count * self.unit.damage[1])
        if self.unit.attack > armor:
            dmgr = (
                cnt[0] * (1 + 0.05 *
                          (self.unit.attack - armor)),
                cnt[1] * (1 + 0.05 *
                          (self.unit.attack - armor))
            )
        else:
            dmgr = (
                cnt[0] / (1 + 0.05 *
                          (armor - self.unit.attack)),
                cnt[1] / (1 + 0.05 *
                          (armor - self.unit.attack))
            )
        dead_count = int(randint(*map(int, dmgr)) // unit_stack.unit.health)
        unit_stack.count = max(0, unit_stack.count - dead_count)
        if (
                Skills.SHOOTER not in self.unit.skills
                and Skills.SHOOTER not in unit_stack.unit.skills
                and Skills.IRRESISTIBLE not in self.unit.skills
                and unit_stack.is_alive
                and not _fight_back
        ):
            unit_stack.attack(self, _fight_back=True)

    @_Decorators.make_used
    def cast(self, unit_stack: 'BattleUnitsStack', *_args) -> None:
        """Defines unit's cast method."""
        if self.unit.cast:
            self.unit.cast(count=self.count, unit_stack=unit_stack)
        else:
            raise BattleUnitsStack.ActionNotAvailable(
                BattleUnitsStack.Actions.CAST)

    @_Decorators.make_used
    def defend(self, *_args) -> None:
        """Unit defends current move."""
        self._end_move.append((self._undefend, (self.unit.armor,), {}))
        self.unit.armor = int(self.unit.armor * 1.3)

    def _undefend(self, armor) -> None:
        """Resets unit's stats after using defend."""
        self.unit.armor = armor

    def wait(self, *_args) -> None:
        """Defines unit's wait method."""
        self.unit.lead = -self.unit.lead
        self._actions[BattleUnitsStack.Actions.WAIT] = False
        self._end_move.append((self._unwait, (), {}))

    def _unwait(self) -> None:
        """Resets unit's stats after using wait."""
        self.unit.lead = abs(self.unit.lead)
