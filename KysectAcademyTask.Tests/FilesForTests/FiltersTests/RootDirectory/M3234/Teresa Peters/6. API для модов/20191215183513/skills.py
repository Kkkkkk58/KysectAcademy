from enum import Enum, auto


class Skills(Enum):
    """
    SHOOTER - the enemy cannot fight back,
    but when enemy attacking, this unit also cannot fight back.
    PRECISE_SHOT - target armor parameter is ignored (as if armor = 0).
    IRRESISTIBLE - the enemy does not fight back.
    """
    SHOOTER = auto()
    PRECISE_SHOT = auto()
    IRRESISTIBLE = auto()
