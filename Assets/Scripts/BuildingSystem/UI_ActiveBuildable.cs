using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private Image _icon;

    private void OnEnable()
    {
        EventBus.SetUIActiveBuildable += setIcon;
    }
    private void OnDisable()
    {
        EventBus.SetUIActiveBuildable -= setIcon;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setIcon(Sprite icon)
    {
        if (icon != null)
        {
            _icon.sprite = icon;
            _icon.enabled = true;
        }
        else
        {
            _icon.enabled = false;
        }
    }
}
