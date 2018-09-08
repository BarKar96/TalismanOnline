using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Assets;

[System.Serializable]
public class Item
{
    public string displayName;
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

    public Player player;
    public int gold = 20;

    //from TalismanBoardScript
    public GameObject shopCanvas;
    public Player[] playerArray;
    public int playerIndex;

    //for my dev
    public Item currentItem = null;
    public Button btnSell;
    public Button btnBuy;
    public Image image;


    void Start()
    {
        shopCanvas = GameObject.Find("Tile").GetComponent<TalismanBoardScript>().shopCanvas;
        setItemsPrices();
        startShop();
        
    }
    public void convertCardsToItems()
    {
        if (whichShop == 0)
        {
            foreach (Card c in player.getItems())
            {
                Item i = new Item();
                i.itemName = c.getName();
                i.displayName = c.display_name;
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
    public void RefreshDisplay()
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

    public void tryTransferItemToOtherShop()
    {
        if (otherShop.gold >= currentItem.price)
        {
            gold += currentItem.price;
            otherShop.gold -= currentItem.price;

            AddItem(currentItem, otherShop);
            RemoveItem(currentItem, this);

            RefreshDisplay();
            otherShop.RefreshDisplay();
            Debug.Log("enough gold");
            clearAfterSuccesfulTransaction();
        }
        Debug.Log("attempted");
    }

    public void clearAfterSuccesfulTransaction()
    {
        currentItem = null;
        image.gameObject.SetActive(false);
        btnBuy.gameObject.SetActive(false);
        btnSell.gameObject.SetActive(false);

    }
    public void clickOnCard(Item item)
    {
        currentItem = item;
        image.sprite = Resources.Load<Sprite>(item.itemName);
        if (whichShop == 0)
        {
            image.gameObject.SetActive(true);
            btnBuy.gameObject.SetActive(false);
            btnSell.gameObject.SetActive(true);
        }
        if (whichShop == 1)
        {
            image.gameObject.SetActive(true);
            btnBuy.gameObject.SetActive(true);
            btnSell.gameObject.SetActive(false);

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
            c.display_name = "Butelka";
        }
        else if (c.getName() == "jablko")
        {
            c.price = 1;
            c.display_name = "Jabłko";
        }
        else if (c.getName() == "zbroja")
        {
            c.price = 1;
            c.display_name = "Zbroja";
        }
        else if (c.getName() == "puszkazlota")
        {
            c.price = 1;
            c.display_name = "Puszka złota";
        }
        else if (c.getName() == "pelnazbrojaplytowa")
        {
            c.price = 1;
            c.display_name = "Pełna zbroja płytowa";
        }
        else if (c.getName() == "helmdemona")
        {
            c.price = 1;
            c.display_name = "Hełm demona";
        }
        else if (c.getName() == "miecz")
        {
            c.price = 1;
            c.display_name = "Miecz";
        }
        else if (c.getName() == "koscianyluk")
        {
            c.price = 1;
            c.display_name = "Kościany łuk";
        }
        else if (c.getName() == "pogromcakrolow")
        {
            c.price = 1;
            c.display_name = "Pogromca królów";
        }
        else if (c.getName() == "talizman")
        {
            c.price = 1;
            c.display_name = "Talizman";
        }
        else if (c.getName() == "topor")
        {
            c.price = 1;
            c.display_name = "Topór";
        }
    }
}