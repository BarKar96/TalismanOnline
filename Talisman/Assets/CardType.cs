using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public enum hero_type
    {
        KAPLAN, ELF, ZLODZIEJ, ZABOJCA, GHUL, TROLL, CZARNOKSIEZNIK, WROZKA,
        CZAROWNICA, WOJOWNIK, KRASNOLUD, DRUID, MINSTREL
    }
    public enum card_type
    {
        BOARDFIELD, ENEMY, ITEM, SPELL
    }
    public enum event_type
    {
        ROLL_DICE, DRAW_CARD, LOSE_HEALTH, ADD_COIN, ENEMY, GAIN_HEALTH
    }
}
