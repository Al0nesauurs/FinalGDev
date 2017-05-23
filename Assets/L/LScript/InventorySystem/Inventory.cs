using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public int slotX, slotY;
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    private bool showInventory;
    private ItemDatabase database;
    // Use this for initialization
	void Start () {
        for(int i = 0; i < slotX * slotY; i++)
        {
            slots.Add(new Item());
        }

        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        inventory.Add(database.items[0]);
        inventory.Add(database.items[1]);
        Debug.Log("check inven." + inventory[0].itemName);
        Debug.Log("check inven." + inventory[1].itemName);
    }

    void OnGUI()
    {
        if(showInventory)
        {
            DrawInventory();
        }
        for (int i =0;i<inventory.Count;i++)
        {
            GUI.Label(new Rect(10, i*20, 200, 50), inventory[i].itemName);
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            showInventory = !showInventory;
        }
    }
    void DrawInventory()
    {
        for (int x =0;x<slotX;x++)
        {
            for (int y=0;y<slotY;y++)
            {

            }
        }
    }
}
