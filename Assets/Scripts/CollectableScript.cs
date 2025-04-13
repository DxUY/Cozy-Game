using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    [SerializeField]  private CollectableType _type;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if(collision.gameObject.tag == "Player")
            {
                PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
                player.inventory.Add(_type);
                Destroy(this.gameObject);
            }
        }
    }
}

public enum CollectableType
{
    NONE, TEST
}
