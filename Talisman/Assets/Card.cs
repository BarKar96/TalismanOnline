using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class Card
    {
        private card_type logic_event;
        private event_type[] events;

        public Card(card_type type)
        {
            this.logic_event = type;
        }
        
        public void completeEvent(Player player, event_type eventtype){
            switch (eventtype)
            {
                case event_type.DRAW_CARD:
                    player.getCards().Add(new Card(card_type.ENEMY));
                    break;
                case event_type.ROLL_DICE:
                    //  Roll player's dice
                    break;
                //  To be extended
            }
        }

        public void onPlayerEvent(Player player)
        {
            switch (this.logic_event)
            {
                case card_type.BOARDFIELD:
                    
                    break;
                case card_type.EVENT:
                    
                    break;
                case card_type.ENEMY:

                    break;
                case card_type.ITEM:
                    player.gold += 1;
                    break;
                case card_type.MAGIC_ITEM:
                    player.current_health -= 3;
                    break;
            }
        }
    }
}
