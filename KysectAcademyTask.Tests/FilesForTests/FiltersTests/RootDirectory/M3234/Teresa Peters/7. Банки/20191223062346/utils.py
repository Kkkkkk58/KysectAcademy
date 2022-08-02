import time
import random
from hashlib import sha256


def gen_id() -> str:
    return sha256(
        '{}{}'.format(time.time(), random.random()).encode('utf-8')
    ).hexdigest()
