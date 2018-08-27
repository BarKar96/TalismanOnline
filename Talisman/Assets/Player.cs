using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player
{
    public string name;
    public Piece playerPiece;
    //Control event on field
    public Card boardField;


   

    //Player item inventory
    private Deck deck;
    public int gold;
    private List<Card> cardsToIterate;
    private List<Card> items;
    private List<Card> spells;

    //statystyki
    public int total_health;
    public int current_health;
    public int strength;

    //modyfikacje
    public int strength_modifier = 0;
    public int health_modifier = 0;
    public Card armor = null;
    public Card weapon = null;


    public bool outerRing { get; set; }
    public bool middleRing { get; set; }
    public bool innerRing { get; set; }
    public Hero hero;
    public int diceResult;
    public bool fieldCheckedOut = false;
    Combat combat;

    //  Online related Variables
    public int NET_RingPos,
                NET_Turn;

    // Canvas
    private GameObject shopCanvas;
    private GameObject mainCanvas;

    public Player(string name, Hero hero)
    {
        combat = GameObject.Find("Combat").GetComponent<Combat>();
        shopCanvas = GameObject.Find("Tile").GetComponent<TalismanBoardScript>().shopCanvas;
        mainCanvas = GameObject.Find("Tile").GetComponent<TalismanBoardScript>().mainCanvas;
        this.name = name;
        this.outerRing = true;
        this.middleRing = false;
        this.innerRing = false;
        this.hero = hero;
        this.total_health = this.current_health = this.hero.hp;
        this.strength = this.hero.strength;
        this.cardsToIterate = new List<Card>(5);
        this.items = new List<Card>();
        this.spells = new List<Card>();
        this.deck = new Deck();
    }

    //Player p = new Player("S", new Hero(Assets.hero_type.CZARNOKSIEZNIK), turn);

    public Player(string name, Hero hero, int net_turn)
    {
        combat = GameObject.Find("Combat").GetComponent<Combat>();
        this.name = name;
        this.outerRing = true;
        this.middleRing = false;
        this.innerRing = false;
        this.hero = hero;
        this.total_health = this.current_health = this.hero.hp;
        this.strength = this.hero.strength;
        this.cardsToIterate = new List<Card>(5);
        this.items = new List<Card>();
        this.spells = new List<Card>();
        this.deck = new Deck();
        this.NET_Turn = net_turn;
    }

    public void rollDice()
    {
        System.Random rnd = new System.Random();
        diceResult = rnd.Next(1, 6);
    }
    public void updateStatistics()
    {
        
        if ( weapon !=null)
        {
            this.strength_modifier = weapon.getSpecialCardEvents()[0].get_roll()[0];
            
        }
        else
        {
            strength_modifier = 0;
           
        }
        if(armor !=null)
        {
            this.health_modifier = armor.getSpecialCardEvents()[0].get_roll()[0];
            
        }
        else
        {
            health_modifier = 0;
           
        }
       
       
    }


    public void iterate_cards()
    {
        var g1 = GameObject.Find("SpecialEvents").GetComponent<SpecialFields>();
        int size = this.cardsToIterate.Count;
        int current = 0;
        

        while (current != size)
        {
            card_type et = cardsToIterate[current].getCard_Type();
            {
                switch (et)
                {
                    case card_type.BOARDFIELD:
                        if (cardsToIterate[current].getName().Equals("Gospoda"))
                        {
                            g1.SetGospodaOn();
                        }
                        else if (cardsToIterate[current].getName().Equals("Cmentarz"))
                        {
                            g1.SetCmentarzOn();
                        }
                        else if (cardsToIterate[current].getName().Equals("Kaplica"))
                        {
                            g1.SetKaplicaOn();
                        }
                        else if (cardsToIterate[current].getName().Equals("Arena"))
                        {
                            g1.SetArenaOn();
                        }
                        else if (cardsToIterate[current].getName().Equals("Las"))
                        {
                            g1.SetLasOn();
                        }
                        else if (cardsToIterate[current].getName().Equals("Czarodziejka"))
                        {
                            g1.SetCzarodziejkaOn();
                        }
                        //
                        else if (cardsToIterate[current].getName().Equals("Wieża wampira"))
                        {
                            g1.SetWiezaWampiraOn();
                        }
                        else if (cardsToIterate[current].getName().Equals("Wieża w Dolinie"))
                        {
                            g1.SetWiezaWDolinieOn();
                        }
                        else if (cardsToIterate[current].getName().Equals("Przepaść"))
                        {
                            g1.SetPrzepascOn();
                        }
                        else if (cardsToIterate[current].getName().Equals("Śmierć"))
                        {
                            g1.SetSmiercOn();
                        }

                        //
                        else if (cardsToIterate[current].getName().Equals("Sklep"))
                        {
                            shopCanvas.gameObject.SetActive(true);
                            //mainCanvas.gameObject.SetActive(false);
                        }
                        break;
                    case card_type.ENEMY:
                        combat.StartCombat(this, cardsToIterate[current]);
                        break;
                    case card_type.SPELL:
                       
                        CardDrawer.spawnMessage(cardsToIterate[current]);
                        this.getSpells().Add(cardsToIterate[current]);
                        break;
                    case card_type.ITEM:
                        if (this.getItems().Count <8)
                        {
                            CardDrawer.spawnMessage(cardsToIterate[current]);
                            this.getItems().Add(cardsToIterate[current]);
                        }
                        else
                        {
                            var go = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
                            go.showFullEQMessage();
                        }
                        break;

                        //case event_type.ADD_COIN:
                        //    this.gold++;
                        //    break;
                        //case event_type.LOSE_HEALTH:
                        //    this.current_health--;
                        //    break;
                        //case event_type.GAIN_HEALTH:
                        //    this.current_health++;
                        //    break;
                        //case event_type.ROLL_DICE:
                        //    //this.rollDice();
                        //    break;

                        //case card_type.DRAW_CARD:
                        //    Card c = deck.drawCard();
                        //    if (cardsToIterate[current].getCard_Type() == card_type.ITEM)
                        //    {
                        //        this.items.Add(c);
                        //    }
                        //if (cards[current].getCard_Type() == card_type.MAGIC_ITEM)
                        //{
                        //    this.items.Add(c);
                        //}
                        //break;


                        //case event_type:
                        //    combat.StartCombat(this, cardsToIterate[current]);
                        //    break;

                }
            }
            current++;
        }
    }
   
    public List<Card> getCards()
    {
        return cardsToIterate;
    }
    public List<Card> getItems()
    {
        return items;
    }
    public List<Card> getSpells()
    {
        return spells;
    }

}
