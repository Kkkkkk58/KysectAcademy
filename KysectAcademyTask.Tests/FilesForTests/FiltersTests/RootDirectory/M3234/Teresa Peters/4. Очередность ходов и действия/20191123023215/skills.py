from enum import Enum, auto


class Skills(Enum):
    """
    SHOOTER - the enemy cannot fight back,
    but when enemy attacking, this unit also cannot fight back.
    PRECISE_SHOT - target armor parameter is ignored (as if armor = 0).
    UNDEAD - unit can be resurrected.
    IRRESISTIBLE - the enemy does not fight back.
    SPLASH - all enemies take damage.
    INF_RESIST - Endless ability to fight back.
    """
    SHOOTER = auto()
    PRECISE_SHOT = auto()
    UNDEAD = auto()
    IRRESISTIBLE = auto()
    SPLASH = auto()
    INF_RESIST = auto()
