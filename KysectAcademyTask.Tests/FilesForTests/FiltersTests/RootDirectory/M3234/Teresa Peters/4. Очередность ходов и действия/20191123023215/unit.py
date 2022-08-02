from typing import Tuple, Union

from .config import UnitsNames, UnitsStats
from .skills import Skills
from .casts import casts_type


class Unit:
    """Base class for unit."""
    def __init__(self, stats=UnitsStats.NOT_DEFINED):
        self.name: UnitsNames = UnitsNames.NOT_DEFINED
        self.health: int = 0
        self.attack: int = 0
        self.armor: int = 0
        self.damage: Tuple[int, int] = (0, 0)
        self.lead: int = 0
        self.skills: Tuple[Skills, ...] = tuple()
        self.cast: casts_type = None
        for key in stats:
            setattr(self, key, stats[key])

    def __repr__(self):
        """Represent unit to human-presentable view."""
        return '<{}>'.format(self.__class__.__name__)


class Arbalester(Unit):
    """Arbalester unit."""
    def __init__(self, stats=UnitsStats.ARBALESTER):
        super().__init__(stats)


class Skeleton(Unit):
    """Skeleton unit."""
    def __init__(self, stats=UnitsStats.SKELETON):
        super().__init__(stats)


class Fury(Unit):
    """Fury unit."""
    def __init__(self, stats=UnitsStats.FURY):
        super().__init__(stats)


class Hydra(Unit):
    """Hydra unit."""
    def __init__(self, stats=UnitsStats.HYDRA):
        super().__init__(stats)


class Angel(Unit):
    """Angel unit."""
    def __init__(self, stats=UnitsStats.ANGEL):
        super().__init__(stats)


class BoneDragon(Unit):
    """Bone Dragon unit."""
    def __init__(self, stats=UnitsStats.BONE_DRAGON):
        super().__init__(stats)


class Demon(Unit):
    """Demon unit."""
    def __init__(self, stats=UnitsStats.DEMON):
        super().__init__(stats)


class Cyclops(Unit):
    """Cyclops unit."""
    def __init__(self, stats=UnitsStats.CYCLOPS):
        super().__init__(stats)


class Griffin(Unit):
    """Griffin unit."""
    def __init__(self, stats=UnitsStats.GRIFFIN):
        super().__init__(stats)


class Shaman(Unit):
    """Shaman unit."""
    def __init__(self, stats=UnitsStats.SHAMAN):
        super().__init__(stats)


class Lich(Unit):
    """Lich unit."""
    def __init__(self, stats=UnitsStats.LICH):
        super().__init__(stats)


units_type = Union[Arbalester, Skeleton, Fury, Hydra, Angel, BoneDragon, Demon,
                   Cyclops, Griffin, Shaman, Lich]
