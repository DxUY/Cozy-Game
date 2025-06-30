using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]ItemData _data;
    public ItemData data
    {
        get { return _data; }
        set { _data = value; }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
