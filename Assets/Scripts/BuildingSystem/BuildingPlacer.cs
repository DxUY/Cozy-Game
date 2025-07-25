using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private BuildableItem _activeBuildableItem;
    [SerializeField] private float _maxDistance = 20f;
    [SerializeField] private Vector2 _mousePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 adjustedPosition = _mousePosition + new Vector2(1, 1);

        if (Vector3.Distance(_mousePosition, transform.position) > _maxDistance || _activeBuildableItem == null)
        {
            EventBus.HidePreview?.Invoke();
            return;
        }
        EventBus.ShowPreview?.Invoke(_activeBuildableItem, adjustedPosition, (bool)EventBus.IsEmpty?.Invoke(adjustedPosition));
        if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Attempting to place building");
                EventBus.Build?.Invoke(adjustedPosition, _activeBuildableItem);
            }
    }
    
}
