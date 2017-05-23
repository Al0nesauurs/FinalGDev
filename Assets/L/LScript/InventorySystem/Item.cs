using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {
    public string itemName;
    public int itemID;
    public string itemDosc;
    public Texture2D itemIcon;
    public int itemPower;
    public int itemSpeed;
    public ItemType itemType;

    public enum ItemType
    {
        Weapon,
        Resource
    }
   
    public Item(string name, int id, string desc, int power,int speed, ItemType type)
    {
        itemName = name;
        itemID = id;
        itemDosc = desc;
        itemPower = power;
        itemSpeed = speed;
        itemType = type;
    }
    
    public Item()
    {

    }
}
