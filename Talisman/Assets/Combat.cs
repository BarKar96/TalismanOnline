using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Combat
{
    public static int rzut_Ataku_gracza = 0;
    public static int skutecznosc_Ataku_gracza = 0;

    public static int rzut_Ataku_istoty = 0;
    public static int skutecznosc_Ataku_istoty = 0;
    public static int  StartCombat(Player p, Card c)
    {
        //wymykanie sie

        //zaklecia
        //rzut ataku gracza
        rzut_Ataku_gracza = rzutAtaku();
        skutecznosc_Ataku_gracza = rzut_Ataku_gracza + p.strength;

        //rzut ataku istoty
        rzut_Ataku_istoty = rzutAtaku();
        skutecznosc_Ataku_istoty = rzut_Ataku_istoty /*+ sila karty*/;

        if (skutecznosc_Ataku_gracza > skutecznosc_Ataku_istoty)
        {
            //istota zostala pokonana wyczyszczenie pola planszy z tej kart;
            return 1;
        }
        else if (skutecznosc_Ataku_gracza < skutecznosc_Ataku_istoty)
        {
            p.current_health--;
            return 2;
        }
        else if (skutecznosc_Ataku_gracza == skutecznosc_Ataku_istoty)
        {
            //istota nie zostaje pokonana
            return 3;
        }
        else
        {
            return 0;
        }



        //porownanie skutecznosci ataku
    }
    public static void wymykanie()
    {

    }
    public static void uzyjZaklecia(Player p)
    {
        CardDrawer.spawnPlayerSpells(p);

    }
    public static int rzutAtaku()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(1, 6);
    }

	
}
