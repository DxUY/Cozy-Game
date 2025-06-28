using UnityEngine;
using System.Collections.Generic;

public class ToolBar_UI : MonoBehaviour
{
    [SerializeField] private List<Slot_UI> _slots = new List<Slot_UI>();
    private Slot_UI _selectedSlot;
    public Slot_UI selectedSlot
    {
        get { return _selectedSlot; }
        set { _selectedSlot = value; }
    }

    public void selectSlot(int index)
    {
        if (_selectedSlot != null)
        {
            _selectedSlot.setHighLight(false); // Remove highlight from previously selected slot

        }
        _selectedSlot = _slots[index];  
        _selectedSlot.setHighLight(true); // Highlight the newly selected slot

        Debug.Log("Selected Slot: " + _selectedSlot);
 
    }

    public void checkAlphaNumericInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectSlot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) selectSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) selectSlot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) selectSlot(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5)) selectSlot(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6)) selectSlot(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7)) selectSlot(6);
        else if (Input.GetKeyDown(KeyCode.Alpha8)) selectSlot(7);
        else if (Input.GetKeyDown(KeyCode.Alpha9)) selectSlot(8);
        else if (Input.GetKeyDown(KeyCode.Alpha0)) selectSlot(9);     

        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectSlot(0);
    }

    // Update is called once per frame
    void Update()
    {
        checkAlphaNumericInput();
    }
}
