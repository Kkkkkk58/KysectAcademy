using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_v2
{
    public enum Feature
    {
        Shooter, // стрелок 
        Precise_shot, // точный выстрел
        Cosset, // нежить
        Oppression, // угнетение (враг не сопротивляется)
        Splash, // удар по всем
        Persistence, // стойкость (бесконечный отпор)
        Vampirism, // вампиризм  (ПОКА НЕ ИСПОЛЬЗОВАЛСЯ)
        Fiery, // имунитет к огню (огонь не может убить дракона :D )
        Icy, // имунитет к заморозке
        Antidote, // имунитет к ядам 
        Sage // мудрец (на него не могут быть применены навыки воздействия на сознание)
    }
}
