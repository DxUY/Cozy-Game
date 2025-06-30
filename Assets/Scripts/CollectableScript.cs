using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Item))]
public class CollectableScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();

                Item item = GetComponent<Item>();
                if (item != null)
                {
                    player.inventory.Add(item);
                    Destroy(this.gameObject);
                    Debug.Log($"Added {item.data.itemName} to inventory.");
                }
                else
                {
                    Debug.LogWarning("Item component not found on the collectable object.");
                }
            }
        }
    }
}

