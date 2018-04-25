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
        private string name;

        public void setName(string n)
        {
            this.name = n;
        }
        public string getName() { return this.name; }

        public Card(card_type type, event_type[] events)
        {
            this.events = events == null ? new event_type[] { } : events;
            this.logic_event = type;
        }

        public Card(string name, card_type type, event_type[] events)
        {
            this.logic_event = type;
            this.events = events == null ? new event_type[] { } : events;
            this.name = name;
        }
        public Card(string name, card_type type, event_type[] events, string desc)
        {
            this.logic_event = type;
            this.events = events == null ? new event_type[] { } : events;
            this.name = name;
            this.description = desc;
        }

        public Card(string name, card_type type, List<event_type> events)
        {
            this.name = name;
            this.logic_event = type;
            if (events == null || events.Count == 0)
            {
                this.events = null;
                return;
            }
            this.events = new event_type[events.Count];
            for(int i =0; i < events.Count; i++)
            {
                this.events[i] = events[i];
            }
        }

        public bool isSpecialField()
        {
            return this.specialField;
        }
        public void setDescription(string d)
        {
            this.description = d;
        }
        public string getDescription()
        {
            return this.description;
        }
        public card_type getCard_Type()
        {
            return logic_event;
        }
        public void AssignSpecial(int[] rolls, event_type et)
        {
            if (!this.specialField)
            {
                this.specialCardEvents = new List<special>();
                this.specialField = true;
            }
            this.specialCardEvents.Add(new special(rolls, et));
        }

        public void specialAction(Player p)
        {
            foreach (special s in this.specialCardEvents)
            {
                if (s.get_roll().Contains(p.diceResult))
                {
                    actOnPlayer(p, s.getEvent());
                }
            }
        }

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
                case event_type.GAIN_HEALTH:
                    p.current_health++;
                    break;
                case event_type.DRAW_CARD:
                    p.getCards().Add(new Card(card_type.ITEM, null));
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
            public string toString()
            {
                string desc = "With each ";
                foreach (int k in rolls)
                    desc += k + " ";
                desc += " will generate " + resulting_event.ToString();
                return desc;
            }
        }
    }



}
