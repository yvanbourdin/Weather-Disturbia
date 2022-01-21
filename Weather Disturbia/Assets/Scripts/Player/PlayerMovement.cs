using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float climbSpeed;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded;
    [HideInInspector]
    public bool isClimbing;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;
    private float verticalMovement;
    private Vector3 gemmeOffset; // get the initial position of the gemme where the projectiles are launched
    private SpriteRenderer gemme; // get the reference to the player's gemme sprite

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of PlayerMovement in the scene");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        gemme = PlayerElementSkills.instance.shootPosition.GetChild(0).GetComponent<SpriteRenderer>();
        gemmeOffset = new Vector3(gemme.transform.localPosition.x, gemme.transform.localPosition.y, 0);
    }

    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.fixedDeltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        {
            isJumping = true;
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);
    }

    // Manage the physics
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers); // Check if the character is on the ground
        MovePlayer(horizontalMovement, verticalMovement);
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        if(!isClimbing)
        {
            // Horizontal movement
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);

            if(isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }
        else
        {
            // Vertical movement
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
        }
        
    }

    void Flip(float _velocity)
    {
        // Flip the sprite and the origine point of the player's projectile
        if (_velocity > 0.1f) // RIGHT
        {
            spriteRenderer.flipX = false;
            PlayerElementSkills.instance.shootPosition.transform.position = new Vector3(gameObject.transform.position.x + 1,
                                                                                        PlayerElementSkills.instance.shootPosition.transform.position.y, 
                                                                                        0);
            // Flip the gemme
            gemme.transform.localPosition = new Vector3(gemmeOffset.x, gemmeOffset.y, 0);
            gemme.flipX = true;
        }
        else if(_velocity < -0.1f) // LEFT
        {
            spriteRenderer.flipX = true;
            PlayerElementSkills.instance.shootPosition.transform.position = new Vector3(gameObject.transform.position.x - 1, 
                                                                                        PlayerElementSkills.instance.shootPosition.transform.position.y, 
                                                                                        0);
            // Flip the gemme
            gemme.transform.localPosition = new Vector3(-gemmeOffset.x, gemmeOffset.y, 0);
            gemme.flipX = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
