using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float walkSpeed = 3f;
    public int Health = 100;
    int currentHealth;
    public EnemyDetection aggroZone;
    private float knockbackForce = 30f;
    private bool isKnockedBack = false;

    private GameObject player;

    private Rigidbody2D enemyRB;
    CharacterController characterController;
    Vector3 enemyPos;
    [SerializeField] private GameObject hitPS = null;
    [SerializeField] private bool IsIdle;

    public enum WalkingDirection { Right, Left };

    private WalkingDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public Animator Anim;

    public WalkingDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                //turn around
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkingDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkingDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }

            }

            _walkDirection = value;
        }
    }

    public bool hasTarget = false;
    public bool HasTarget { get { return hasTarget; }
        private set
        {
            hasTarget = value;
            Anim.SetBool("IsTargeting", value);
            
        }
    }

    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        characterController = GetComponent<CharacterController>();
        Anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        enemyPos = this.transform.position;


        HasTarget = aggroZone.detectedColls.Count > 0;
    }
    private void FixedUpdate()
    {
        
        if (!characterController.IsGrounded() || characterController.IsOnWall())
        {
            ChangeDirection();
        }
        if (isKnockedBack)
        {
            Debug.Log("KNOCKED");
            enemyRB.velocity = Vector2.zero;
            StartCoroutine(EnableMovementAfterDelay());
        }
        else
        {
            enemyRB.velocity = new Vector2(walkSpeed * walkDirectionVector.x, enemyRB.velocity.y);
        }
  
    }

    private void ChangeDirection()
    {
    //    Debug.Log("ChangeDirection called. WalkDirection is " + WalkDirection);

        if (WalkDirection == WalkingDirection.Right)
        {
            WalkDirection = WalkingDirection.Left;
            
        }
        else if (WalkDirection == WalkingDirection.Left)
        {
            WalkDirection = WalkingDirection.Right;
            
        }
    }
    private void TrackPlayer()
    {
        if(player.transform.position.x > transform.position.x)
        {
            WalkDirection = WalkingDirection.Right;
        }
        else
        {
            WalkDirection = WalkingDirection.Left;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial walking direction to Right
        
        currentHealth = Health;
        WalkDirection = WalkingDirection.Right;
        Anim.SetBool("IsIdle", IsIdle);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Anim.SetTrigger("Hurt");

        isKnockedBack = true;
        TrackPlayer();

        SpawnParticles(enemyPos);
        if(currentHealth <= 0)
        {
            Die();
        }

    }
    private IEnumerator EnableMovementAfterDelay()
    {
        Vector2 knockbackDirection = (transform.position - player.transform.position).normalized;
  
        enemyRB.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        yield return new WaitForSecondsRealtime(0.5f);

        isKnockedBack = false;
    }

    public void SpawnParticles(Vector3 position)
    {
        Instantiate(hitPS, position,new Quaternion(0,0,0,0));
    }

 
   void Die()
    {
        
        Debug.Log("DIED");
        Anim.SetBool("IsDead", true);



        GetComponent<CapsuleCollider2D>().enabled = false;
        enemyRB.bodyType = RigidbodyType2D.Static;
        this.enabled = false;

    }
}
