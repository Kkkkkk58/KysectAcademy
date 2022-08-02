from enum import Enum, auto
from typing import Dict, Any, Tuple

from .skills import Skills
from .casts import *


class UnitsNames(Enum):
    """Defines unit names with nums."""
    NOT_DEFINED = auto()
    ARBALESTER = auto()
    SKELETON = auto()
    FURY = auto()
    HYDRA = auto()
    ANGEL = auto()
    BONE_DRAGON = auto()
    DEMON = auto()
    CYCLOPS = auto()
    GRIFFIN = auto()
    SHAMAN = auto()
    LICH = auto()


def _get_stats_dict(
        name: UnitsNames, health: int, attack: int, armor: int,
        damage: Tuple[int, int], lead: int, skills: Tuple[Skills, ...],
        cast: casts_type
) -> Dict[str, Any]:
    """Returns config dict with stats of unit.

    :return: dict[<stat name> e.g. 'health', <stat value>]
    """
    return {
        'name': name,
        'health': health,
        'attack': attack,
        'armor': armor,
        'damage': damage,
        'lead': lead,
        'skills': skills,
        'cast': cast
    }


class UnitsStats:
    """Config for units stats."""
    NOT_DEFINED = _get_stats_dict(
        UnitsNames.NOT_DEFINED, 0, 0, 0, (0, 0), 0, tuple(), None
    )
    ARBALESTER = _get_stats_dict(
        UnitsNames.ARBALESTER, 10, 4, 4, (2, 8), 8,
        (Skills.SHOOTER, Skills.PRECISE_SHOT), None
    )
    SKELETON = _get_stats_dict(
        UnitsNames.SKELETON, 5, 1, 2, (1, 1), 10, tuple(), None
    )
    FURY = _get_stats_dict(
        UnitsNames.FURY, 16, 5, 3, (5, 7), 16, (Skills.IRRESISTIBLE,), None
    )
    HYDRA = _get_stats_dict(
        UnitsNames.HYDRA, 80, 15, 12, (7, 14), 7,
        (Skills.IRRESISTIBLE,), None
    )
    ANGEL = _get_stats_dict(
        UnitsNames.ANGEL, 180, 27, 27, (45, 45), 11, tuple(), Punish()
    )
    BONE_DRAGON = _get_stats_dict(
        UnitsNames.BONE_DRAGON, 150, 27, 28, (15, 30), 11, tuple(), Curse()
    )
    DEMON = _get_stats_dict(
        UnitsNames.DEMON, 166, 27, 25, (36, 66), 11, tuple(), Weak()
    )
    CYCLOPS = _get_stats_dict(
        UnitsNames.CYCLOPS, 85, 20, 15, (18, 26), 10, (Skills.SHOOTER,), None
    )
    GRIFFIN = _get_stats_dict(
        UnitsNames.GRIFFIN, 30, 7, 5, (5, 10), 15, tuple(), None
    )
    SHAMAN = _get_stats_dict(
        UnitsNames.SHAMAN, 40, 12, 10, (7, 12), 10, tuple(), Speed()
    )
    LICH = _get_stats_dict(
        UnitsNames.LICH, 50, 15, 15, (12, 17), 10,
        (Skills.SHOOTER,), Resurrection()
    )
