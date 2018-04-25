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
        BOARDFIELD, EVENT, ENEMY, ITEM, MAGIC_ITEM, FRIEND
    }
    public enum event_type
    {
        ROLL_DICE, DRAW_CARD, LOSE_HEALTH, ADD_COIN, COMBAT, GAIN_HEALTH
    }
}
