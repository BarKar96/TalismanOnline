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

    public Hero(int index)
    {
        
        fillHeroInformation(index);
    }

    public void fillHeroInformation(int index)
    {
        switch (index)
        {
            case 0:
                name = "Mnich";
                strength = 1;
                power = 1;
                luck = 5;
                hp = 10;
                startingLocation = 7;
                break;
            case 1:
                name = "Warrior";
                strength = 1;
                power = 1;
                luck = 5;
                hp = 1;
                startingLocation = 1;
                break;
            default:
               
                break;
        }
    }
	
}
