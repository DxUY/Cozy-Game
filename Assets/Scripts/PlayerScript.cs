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

        [SerializeField] private Tilemap _groundTileMap;

        [SerializeField] private Tile _highlightTile;

        [SerializeField] private Tile _grassTile;

        [SerializeField]  public Inventory _inventory;
        public Inventory inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

    private void Awake()
    {
        _inventory = new Inventory(15);

    }

    void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
        if (_mousePosition != null)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;

            Vector2 cellSize = _groundTileMap.cellSize; // Đảm bảo kiểm tra giá trị này trong Inspector
            Vector3 adjustedPosition = worldPosition + new Vector3(1, 1, 0);
            Debug.Log(cellSize);
            Vector3Int cellPosition = _groundTileMap.WorldToCell(adjustedPosition);

            

            _groundTileMap.SetTile(cellPosition, _highlightTile);
            Debug.Log("Mouse World Pos: " + worldPosition + " Adjusted Pos: " + adjustedPosition + " Cell Pos: " + cellPosition);
            


        }   

            // Remove 'float' keyword here since variables are already declared as class members
            _moveX = Input.GetAxisRaw("Horizontal");
            _moveY = Input.GetAxisRaw("Vertical");
            animateMovement();
            // Fix this line - was using _moveY twice instead of _moveX and _moveY
            _moveVelocity = new Vector2(_moveX, _moveY) * moveSpeed;



        if (Input.GetMouseButtonDown(0))
            {
            if ((bool)EventBus.GetTileAvailable?.Invoke(_mousePosition))
                {
                    EventBus.Plowed?.Invoke(_mousePosition);
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
