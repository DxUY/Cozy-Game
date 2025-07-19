using System;
using UnityEngine;

public class ChestScript : MonoBehaviour, IInteractables
{
    public Vector3Int tilePosition { get; set; }

    [SerializeField] private Inventory _inventory;

    [SerializeField] private Sprite _chestOpenSprite;
    [SerializeField] private Sprite _chestClosedSprite;

    public void Interact()
    {
        Debug.Log("Chest interacted with at position: " + tilePosition);
        GetComponent<SpriteRenderer>().sprite = _chestOpenSprite;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
