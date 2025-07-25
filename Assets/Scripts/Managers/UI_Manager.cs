using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private Dictionary<string, Inventory_UI> _inventoryUiDict = new Dictionary<string, Inventory_UI>();

    [SerializeField] private List<Inventory_UI> _inventoryUiList = new List<Inventory_UI>();

    [SerializeField] private GameObject _inventoryPanel;
    private bool _inventoryVisible = false;

    [SerializeField] public static Slot_UI _draggingSlot;
    [SerializeField] public static Image _draggingIcon;
    private Slot_UI _currentSlot;

    [SerializeField] private Slider _caughtSlider;
    [SerializeField] private Slider _tensionSlider;


    private void Awake()
    {
        initialized();
        _inventoryPanel.SetActive(false);
        selectSlot(0, "ToolBar");


        //setUpAllInventory();
    }

    private void OnEnable()
    {
        EventBus.GetCurrentSlot += getCurrentSlot;
        EventBus.SetUpAllInventory += setUpAllInventory;
        EventBus.FishingUI += startFishing;

    }

    private void OnDisable()
    {
        EventBus.GetCurrentSlot -= getCurrentSlot;
        EventBus.SetUpAllInventory -= setUpAllInventory;
        EventBus.FishingUI -= startFishing;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            toggleInvetory();
        }
        checkAlphaNumericInput();

    }

    public Inventory_UI getByName(string inventoryName)
    {
        if (_inventoryUiDict.ContainsKey(inventoryName)) return _inventoryUiDict[inventoryName];
        else return null;
    }

    private void initialized()
    {
        foreach (Inventory_UI ui in _inventoryUiList)
        {
            if (!_inventoryUiDict.ContainsKey(ui.inventoryName))
            {
                _inventoryUiDict.Add(ui.inventoryName, ui);
            }
        }


    }

    public void toggleInvetory()
    {
        if (_inventoryPanel != null)
        {
            if (_inventoryVisible == false)
            {
                _inventoryPanel.SetActive(true);
                setUpInventory("Backpack");
                _inventoryVisible = true;

            }
            else
            {
                _inventoryPanel.SetActive(false);
                _inventoryVisible = false;
            }
        }

    }


    public void setUpInventory(string inventoryName)
    {
        if (_inventoryUiDict.ContainsKey(inventoryName))
        {
            _inventoryUiDict[inventoryName].setUp();
        }
    }

    public void setUpAllInventory()
    {
        foreach (KeyValuePair<string, Inventory_UI> pair in _inventoryUiDict)
        {
            pair.Value.setUp();
        }
    }

    public void selectSlot(int index, string iventoryName)
    {
        if (_currentSlot != null)
        {
            _currentSlot.setHighLight(false);

        }
        _currentSlot = _inventoryUiDict[iventoryName].Slots[index];
        _currentSlot.setHighLight(true);
        Debug.Log("Selected slot " + _currentSlot);
    }

    public void checkAlphaNumericInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectSlot(0, "ToolBar");
        else if (Input.GetKeyDown(KeyCode.Alpha2)) selectSlot(1, "ToolBar");
        else if (Input.GetKeyDown(KeyCode.Alpha3)) selectSlot(2, "ToolBar");
        else if (Input.GetKeyDown(KeyCode.Alpha4)) selectSlot(3, "ToolBar");
        else if (Input.GetKeyDown(KeyCode.Alpha5)) selectSlot(4, "ToolBar");
        else if (Input.GetKeyDown(KeyCode.Alpha6)) selectSlot(5, "ToolBar");
        else if (Input.GetKeyDown(KeyCode.Alpha7)) selectSlot(6, "ToolBar");
        else if (Input.GetKeyDown(KeyCode.Alpha8)) selectSlot(7, "ToolBar");
        else if (Input.GetKeyDown(KeyCode.Alpha9)) selectSlot(8, "ToolBar");
        else if (Input.GetKeyDown(KeyCode.Alpha0)) selectSlot(9, "ToolBar");


    }

    public Slot_UI getCurrentSlot()
    {

        return _currentSlot;
    }


    public void startFishing()
    {
        if (_caughtSlider != null && _tensionSlider != null)
        {
            _caughtSlider.gameObject.SetActive(true);
            _tensionSlider.gameObject.SetActive(true);
            EventBus.Fishing.Invoke();
        }
    }
    
}
