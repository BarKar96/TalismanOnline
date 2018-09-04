using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Assets;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public int price = 1;
}

public class ShopScrollList : MonoBehaviour
{

    public List<Item> itemList;
    public Transform contentPanel;
    public ShopScrollList otherShop;
    public Text myGoldDisplay;
    public SimpleObjectPool buttonObjectPool;
    public int whichShop;
    public Image image;
    public Player player;
    public int gold = 20;

    //from TalismanBoardScript
    public GameObject shopCanvas;
    public Player[] playerArray;
    public int playerIndex;


    void Start()
    {
        shopCanvas = GameObject.Find("Tile").GetComponent<TalismanBoardScript>().shopCanvas;
        startShop();
        setItemsPrices();
    }
    public void convertCardsToItems()
    {
        if (whichShop == 0)
        {
            foreach (Card c in player.getItems())
            {
                Item i = new Item();
                i.itemName = c.getName();
                i.price = c.price;
                i.icon = Resources.Load<Sprite>(c.getName());
                itemList.Add(i);
            }
        }
        
    }
    public void addGoldToShops()
    {
        if (whichShop == 0)
        {
            gold = player.gold;
        }
        if (whichShop == 1)
        {
            gold = 30;
        }
    }
    public void startShop()
    {

        playerArray = GameObject.Find("Tile").GetComponent<TalismanBoardScript>().playerArray;
        playerIndex = GameObject.Find("Tile").GetComponent<TalismanBoardScript>().playerIndex;
        player = playerArray[playerIndex];
        addGoldToShops();
        convertCardsToItems();
        RefreshDisplay();
    }
    void RefreshDisplay()
    {
        myGoldDisplay.text = "Gold: " + gold.ToString();
        RemoveButtons();
        AddButtons();
    }

    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }

    private void AddButtons()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            Item item = itemList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel, false);

            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(item, this);
        }
    }

    public void TryTransferItemToOtherShop(Item item)
    {
        clickOnCard(item);
        if (otherShop.gold >= item.price)
        {
            gold += item.price;
            otherShop.gold -= item.price;

            AddItem(item, otherShop);
            RemoveItem(item, this);

            RefreshDisplay();
            otherShop.RefreshDisplay();
            Debug.Log("enough gold");
        }
        Debug.Log("attempted");
    }
    public void clickOnCard(Item item)
    {
        image.sprite = Resources.Load<Sprite>(item.itemName);
        if (whichShop == 0)
        {

        }
        if (whichShop == 1)
        {

        }
    }

    void AddItem(Item itemToAdd, ShopScrollList shopList)
    {
        shopList.itemList.Add(itemToAdd);
    }

    private void RemoveItem(Item itemToRemove, ShopScrollList shopList)
    {
        for (int i = shopList.itemList.Count - 1; i >= 0; i--)
        {
            if (shopList.itemList[i] == itemToRemove)
            {
                shopList.itemList.RemoveAt(i);
            }
        }
    }
    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////
    /// </summary>
    public void exitFromShop()
    {
        shopCanvas.gameObject.SetActive(false);
    }

    public void setItemsPrices()
    {
        var go = GameObject.Find("Tile").GetComponent<TalismanBoardScript>();
        foreach (Card c in go.deckOfCards.fullDeck)
        {
            setSingleItemPrice(c);
        }
        
    }

    public void setSingleItemPrice(Card c)
    {
        if (c.getName() == "butelka")
        {
            c.price = 1;
        }
        else if (c.getName() == "jablko")
        {
            c.price = 1;
        }
        else if (c.getName() == "zbroja")
        {
            c.price = 1;
        }
        else if (c.getName() == "puszkazlota")
        {
            c.price = 1;
        }
        else if (c.getName() == "pelnazbroja")
        {
            c.price = 1;
        }
        else if (c.getName() == "helmdemona")
        {
            c.price = 1;
        }
        else if (c.getName() == "miecz")
        {
            c.price = 1;
        }
        else if (c.getName() == "koscianyluk")
        {
            c.price = 1;
        }
        else if (c.getName() == "pogromcakrolow")
        {
            c.price = 1;
        }
        else if (c.getName() == "talizman")
        {
            c.price = 1;
        }
        else if (c.getName() == "topor")
        {
            c.price = 1;
        }
    }
}