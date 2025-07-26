using System.Collections.Generic;
using UnityEngine;

public class BuildingSelector : MonoBehaviour
{
    [SerializeField] private List<BuildableItem> _buildableItems = new List<BuildableItem>();
    [SerializeField] private int _activeItemIndex;

    private void OnEnable()
    {
        EventBus.GetNextBuildableItem += nextItem;
    }

    private void OnDisable()
    {
        EventBus.GetNextBuildableItem -= nextItem;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public BuildableItem nextItem()
    {
        _activeItemIndex = (_activeItemIndex + 1) % _buildableItems.Count;
        return _buildableItems[_activeItemIndex];
    }
}
