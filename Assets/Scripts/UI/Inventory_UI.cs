using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    private bool isOpen = false;
    [SerializeField] private PlayerScript player;
    [SerializeField] private List<Slot_UI> slots = new List<Slot_UI>();
  

    void Start()
    {
        inventoryPanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            toggleInvetory();
        }
    }

    public void toggleInvetory()
    {
        if (isOpen == false)
        {
            inventoryPanel.SetActive(true);
            setUp();
            isOpen = true;

        }
        else
        {
            inventoryPanel.SetActive(false);
            isOpen = false;
        }
    }

    void setUp()
    {
        if(slots.Count == player.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if(player.inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(player.inventory.slots[i]);
                }
                else
                {
                    slots[i].setEmpty();
                }
            }
        }
        
    }
}
