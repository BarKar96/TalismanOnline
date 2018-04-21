using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class Card
    {
        private card_type logic_event;
        private event_type[] events;
        private bool specialField = false;
        private List<special> specialCardEvents;
        public event_type[] getEvents() { return events; }
        private string description;

        public Card(card_type type, event_type[] events)
        {
            this.logic_event = type;
            this.events = events;
        }

        public void setDescription(string d)
        {
            this.description = d;
        }
        public string getDescription()
        {
            return this.description;
        }

        public void AssignSpecial(int[] rolls, event_type et)
        {
            if (!this.specialField)
                this.specialField = true;
            this.specialCardEvents = new List<special>();
            this.specialCardEvents.Add(new special(rolls, et));
        }

        public void specialAction(Player p)
        {
            foreach (special s in this.specialCardEvents)
            {
                if (s.get_roll().Contains(p.gold))
                {
                    actOnPlayer(p, s.getEvent());
                }
            }
        }
        // comment
        private void actOnPlayer(Player p, event_type et)
        {
            switch (et)
            {
                case event_type.ADD_COIN:
                    p.gold++;
                    break;
                case event_type.LOSE_HEALTH:
                    p.current_health--;
                    break;
                case event_type.ROLL_DICE:
                    //p.rollDice();
                    break;
                case event_type.DRAW_CARD:
                    p.getCards().Add(new Card(card_type.ITEM, null));
                    break;
            }
        }
        public void iterateEvents(Player p)
        {
            if (this.specialField)
                specialAction(p);

            foreach (event_type et in events)
            {
                switch (et)
                {
                    case event_type.ADD_COIN:
                        p.gold++;
                        break;
                    case event_type.LOSE_HEALTH:
                        p.current_health--;
                        break;
                    case event_type.GAIN_HEALTH:
                        p.current_health++;
                        break;
                    case event_type.ROLL_DICE:
                        p.rollDice();
                        Debug.Log("rzut kostka z eventu" + p.diceResult);
                        break;
                    case event_type.DRAW_CARD:
                        //  Throws Error
                        //p.getCards().Add(new Card(card_type.ITEM, new event_type[] { event_type.LOSE_HEALTH}));
                        break;
                }
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
        private class special
        {
            public special(int[] rolls, event_type et)
            {
                this.rolls = rolls;
                this.resulting_event = et;
            }
            private int[] rolls;
            public int[] get_roll() { return rolls; }
            private event_type resulting_event;
            public event_type getEvent() { return resulting_event; }
        }
    }



}
