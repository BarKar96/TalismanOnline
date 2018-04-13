using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public enum card_type
    {
        BOARDFIELD, EVENT, ENEMY, ITEM, MAGIC_ITEM
    }
    public enum event_type
    {
        ROLL_DICE, DRAW_CARD, LOSE_HEALTH,
    }
}
