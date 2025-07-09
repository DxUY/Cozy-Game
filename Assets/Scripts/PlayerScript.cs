using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 _moveVelocity;
    private float _moveX;
    private float _moveY;
    [SerializeField] private float _speedLimiter = 0.2f;
    private Animator animator;
    private Vector3 _mousePosition;

    [SerializeField] private Tile _grassTile;

    [SerializeField]  private InventoryManager _inventoryManager;
    [SerializeField] private string _selectedItemName;
    public InventoryManager inventoryManager
    {
        get { return _inventoryManager; }
        set { _inventoryManager = value; }
    }

void Awake()
{
    _inventoryManager = GetComponent<InventoryManager>();
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
        
}

    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 adjustedPosition = _mousePosition + new Vector3(1, 1, 0);


        // Remove 'float' keyword here since variables are already declared as class members
        _moveX = Input.GetAxisRaw("Horizontal");
        _moveY = Input.GetAxisRaw("Vertical");
        animateMovement();
        // Fix this line - was using _moveY twice instead of _moveX and _moveY
        _moveVelocity = new Vector2(_moveX, _moveY) * moveSpeed;




        if (Input.GetMouseButtonDown(0))
        {
            _selectedItemName = EventBus.GetCurrentSlot?.Invoke().itemName;
            if ((bool)EventBus.GetTileAvailable?.Invoke(adjustedPosition) && _selectedItemName != null && _selectedItemName.Contains("_hoe"))
            {
                EventBus.Plowed?.Invoke(adjustedPosition);
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _selectedItemName = EventBus.GetCurrentSlot?.Invoke().itemName;
            if (_selectedItemName.Contains("_seed"))
            {
                EventBus.PlantSeed?.Invoke(adjustedPosition, _selectedItemName);
            }
            else if (_selectedItemName.Contains("_watercan"))
            {
                EventBus.WaterPlant.Invoke(adjustedPosition);
            }
            else if (_selectedItemName.Contains("_fishingrod"))
            {
                EventBus.FishingUI.Invoke();
            }
                
        }
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        //Debug.Log("Moving");
        if(_moveX!=0 || _moveY!=0)
        {
            if(_moveX != 0 && _moveY != 0)
            {
                _moveVelocity *= _speedLimiter;
            }
            rb.linearVelocity = _moveVelocity;
        }
        else
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
    }

private void animateMovement()
{
    if (animator != null)
    {
        if (_moveX != 0 || _moveY != 0)
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("horizontal", _moveX);
            animator.SetFloat("vertical", _moveY);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
}
