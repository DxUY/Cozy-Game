    using Mono.Cecil.Cil;
    using UnityEngine;

    public class PlayerScript : MonoBehaviour
    {
        private Rigidbody2D rb;
        [SerializeField] private float moveSpeed = 5f;
        private Vector2 _moveVelocity;
        private float _moveX;
        private float _moveY;
        [SerializeField] private float _speedLimiter = 0.2f;
        private Animator animation;
        private bool _isFacingLeft = true;

        [SerializeField]  public Inventory _inventory;
        public Inventory inventory
        {
            get
            {
                return _inventory;
            }

            set
            {
                _inventory = value;
            }
        }

    private void Awake()
    {
        _inventory = new Inventory(10);

    }

    void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animation = GetComponent<Animator>();
        }

        void Update()
        {
            // Remove 'float' keyword here since variables are already declared as class members
            _moveX = Input.GetAxisRaw("Horizontal");
            _moveY = Input.GetAxisRaw("Vertical");

            // Fix this line - was using _moveY twice instead of _moveX and _moveY
            _moveVelocity = new Vector2(_moveX, _moveY) * moveSpeed;

            animation.SetFloat("Speed", Mathf.Abs(_moveX) + Mathf.Abs(_moveY));

            if(_moveX<0 && !_isFacingLeft)
            {
                Flip();
            }
            else if(_moveX > 0 && _isFacingLeft)
            {   
                Flip();
            }



        if (Input.GetKeyDown(KeyCode.Space))
            {
            Vector3 position;
            if (_isFacingLeft)
            {
                 position = new Vector3(Mathf.Round(transform.position.x - 0.5f),
                        transform.position.y , 0);
            }
            else
            {
                 position = new Vector3(Mathf.Round(transform.position.x + 0.5f),
                        transform.position.y , 0);
            }

            if ((bool)EventBus.GetTileAvailable?.Invoke(position))
                {
                    EventBus.Plowed?.Invoke(position);
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

        private void Flip()
        {
            _isFacingLeft= !_isFacingLeft;
            Vector2 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;
        }
    }
