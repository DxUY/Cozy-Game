using UnityEngine;
using UnityEngine.Tilemaps;

public class PreviewLayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _previewRenderer;
    [SerializeField] private Tilemap _constructionTileMap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {

        EventBus.ShowPreview += showPreview;
        EventBus.HidePreview += hidePreview;

    }

    private void OnDisable()
    {

        EventBus.ShowPreview -= showPreview;
        EventBus.HidePreview -= hidePreview;

    }

    public void showPreview(BuildableItem buildableItem, Vector3 worldPosition, bool isValid)
    {
        var tileCoords = _constructionTileMap.WorldToCell(worldPosition);
        _previewRenderer.enabled = true;
        _previewRenderer.transform.position = _constructionTileMap.GetCellCenterWorld(tileCoords);
        _previewRenderer.transform.localScale = new Vector3(2,2,1);
        _previewRenderer.sprite = buildableItem.PreviewSprite;
        _previewRenderer.color = isValid ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);
    }

    public void hidePreview()
    {
        _previewRenderer.enabled = false;
    }
}
