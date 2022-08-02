from typing import Optional, Union


class CastsFactors:
    Punish = 12
    Curse = 12
    Weak = 12
    Speed = 1.4
    Resurrection = 100


class Cast:
    def __call__(self, *args, **kwargs) -> None:
        pass

    def __repr__(self):
        """Represent cast to human-presentable view."""
        return '<Cast {}>'.format(self.__class__.__name__)


# noinspection PyUnresolvedReferences
class Punish(Cast):
    """Increases attack by 12."""
    def __call__(self, unit_stack: 'BattleUnitsStack',
                 *args, **kwargs) -> None:
        unit_stack.unit.attack += CastsFactors.Punish


# noinspection PyUnresolvedReferences
class Curse(Cast):
    """Decreases attack by 12."""
    def __call__(self, unit_stack: 'BattleUnitsStack',
                 *args, **kwargs) -> None:
        unit_stack.unit.attack = max(
            0, unit_stack.unit.attack - CastsFactors.Curse
        )


# noinspection PyUnresolvedReferences
class Weak(Cast):
    """Decreases armor by 12."""
    def __call__(self, unit_stack: 'BattleUnitsStack',
                 *args, **kwargs) -> None:
        unit_stack.unit.armor = max(
            0, unit_stack.unit.armor - CastsFactors.Weak
        )


# noinspection PyUnresolvedReferences
class Speed(Cast):
    """Increases lead by 40%."""
    def __call__(self, unit_stack: 'BattleUnitsStack',
                 *args, **kwargs) -> None:
        unit_stack.unit.lead = int(unit_stack.unit.lead * CastsFactors.Speed)


# noinspection PyUnresolvedReferences
class Resurrection(Cast):
    """Resurrect 100 hp per 1 unit."""
    def __call__(self, count: int, unit_stack: 'BattleUnitsStack',
                 *args, **kwargs) -> None:
        """
        :param count: Count of healers.
        :param unit_stack: target stack.
        """
        hp = int(count * CastsFactors.Resurrection)
        unit_stack.count = min(
            unit_stack.init_count,
            unit_stack.count + int(hp // unit_stack.unit.health)
        )


casts_type = Optional[Union[Punish, Curse, Weak, Speed, Resurrection]]
