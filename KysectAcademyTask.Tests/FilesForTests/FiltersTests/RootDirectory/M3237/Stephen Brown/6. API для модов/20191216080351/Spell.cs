using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_v2
{
    public enum Spell
    {
        Punishing_blow, // карающий удар
        Curse, // проклятие
        Weakening, // ослабление
        Acceleration, // ускорение
        Resurrection, // воскрещение
        Poisoning, // отравление           (ПОКА НЕ ИСПОЛЬЗОВАЛСЯ)
        Arcon, // поджигание
        Freezing, // замораживание   (ПОКА НЕ ИСПОЛЬЗОВАЛСЯ)
        Stun, // оглушение        (ПОКА НЕ ИСПОЛЬЗОВАЛСЯ)
        Persuasion, // убеждение (переманивает противника на свою сторону)
        Suicide, // жертвует собой, нанося урон, пропорциональный имеющимся жизням
        Treacherous, // вероломство (высасывает жизнь из своего союзника) Кол-во юнитов * 10
        Convenience, // удобство (сбрасывает броню, повышая урон)  (ПОКА НЕ ИСПОЛЬЗОВАЛСЯ)
        Invisibility, // невидимость (юнит не может атаковать и не может быть атакован)     
        Call // призыв духа
    }
}
