using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero
{
    public string name;
    public int strength;
    public int power;
    public int luck;
    public int hp;
    public int startingLocation;

    public Hero(hero_type index)
    {
        
        fillHeroInformation(index);
    }

    public void fillHeroInformation(hero_type ht)
    {
        switch (ht)
        {
            case hero_type.ZLODZIEJ:
                name = "Złodziej";
                strength = 3;
                power = 3;
                luck = 2;
                hp = 4;
                startingLocation = 1;
                break;
            case hero_type.MINSTREL:
                name = "Minstrel";
                strength = 2;
                power = 4;
                luck = 5;
                hp = 4;
                startingLocation = 2;
                break;
            case hero_type.DRUID:
                name = "Druid";
                strength = 3;
                power = 4;
                luck = 4;
                hp = 4;
                startingLocation = 3;
                break;
            case hero_type.KRASNOLUD:
                name = "Krasnolud";
                strength = 3;
                power = 3;
                luck = 5;
                hp = 5;
                startingLocation = 4;
                break;
            case hero_type.WOJOWNIK:
                name = "Wojownik";
                strength = 4;
                power = 2;
                luck = 1;
                hp = 5;
                startingLocation = 5;
                break;
            case hero_type.CZAROWNICA:
                name = "Czarownica";
                strength = 2;
                power = 4;
                luck = 3;
                hp = 4;
                startingLocation = 6;
                break;
            case hero_type.WROZKA:
                name = "Wróżka";
                strength = 2;
                power = 4;
                luck = 2;
                hp = 4;
                startingLocation = 7;
                break;
            case hero_type.CZARNOKSIEZNIK:
                name = "Czarnoksiężnik";
                strength = 2;
                power = 5;
                luck = 3;
                hp = 4;
                startingLocation = 8;
                break;
            case hero_type.TROLL:
                name = "Troll";
                strength = 6;
                power = 1;
                luck = 1;
                hp = 6;
                startingLocation = 9;
                break;
            case hero_type.GHUL:
                name = "Ghul";
                strength = 2;
                power = 4;
                luck = 4;
                hp = 4;
                startingLocation = 10;
                break;
            case hero_type.ZABOJCA:
                name = "Zabójca";
                strength = 3;
                power = 3;
                luck = 3;
                hp = 4;
                startingLocation = 11;
                break;
            case hero_type.ELF:
                name = "Elf";
                strength = 3;
                power = 4;
                luck = 3;
                hp = 4;
                startingLocation = 12;
                break;
            case hero_type.KAPLAN:
                name = "Kapłan";
                strength = 2;
                power = 4;
                luck = 5;
                hp = 4;
                startingLocation = 13;
                break;
        }
    }
	
}
