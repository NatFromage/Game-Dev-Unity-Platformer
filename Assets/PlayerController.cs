using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed, jumpDist;
    public Transform groundPoint;
    public CapsuleCollider2D capsuleCollider2D;
    public bool Attacking;

    private DamageController damageController;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask playerLayer;

    private float inputX;
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        damageController = GetComponent<DamageController>();
   }

    public bool IsGrounded()
    {
        return Physics2D.Raycast(capsuleCollider2D.bounds.center, Vector2.down, capsuleCollider2D.bounds.extents.y + 0.4f, platformLayer);
    }//Checking if player is grounded, shooting a ray down vertically, using the distance from center to bottom of the player to work out whether the ray hits the ground in that 




    void Update()
    {


        theRB.velocity = new Vector2(inputX * moveSpeed, theRB.velocity.y);


 

        if (theRB.velocity.x > 0f)
        {
            transform.localScale = Vector3.one;
        }
        else if (theRB.velocity.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        anim.SetFloat("velocity", Math.Abs(theRB.velocity.x));
        anim.SetBool("IsGrounded", IsGrounded());

    }



    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded() == true)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpDist);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetTrigger("attack");
        }
    }

    public void StartAirAttack()
    {
        Attacking = true;
    }

    public void FinishAirAttack()
    {
        Attacking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !Attacking)
        {
          
            damageController.Damage(1);
        }
    }

    public void GameOver()
    {
        
        SceneManager.LoadScene(0);
    }

}
