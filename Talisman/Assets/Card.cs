using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    class Card
    {
        private card_type logic_event;
        private event_type[] events;

        public Card(card_type type)
        {
            this.logic_event = type;
        }
        
        public void completeEvent(Player player, event_type eventtype){
            switch (event_type)
            {
                case event_type.DRAW_CARD:
                    player.get
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

                    break;
                case card_type.MAGIC_ITEM:

                    break;
            }
        }
    }
}
