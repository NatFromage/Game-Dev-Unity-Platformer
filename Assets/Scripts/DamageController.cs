using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private PlayerController player;
    [SerializeField] public Rigidbody2D playerRB;


 private float knockbackForce = 20;
    public void Damage(int damage)
    {
        healthSystem.playerHealth -= damage;
        healthSystem.UpdateHealth();


        player.anim.SetTrigger("IsHurt");

        

        Vector2 knockbackDirection = (transform.position - playerRB.transform.position).normalized;
        playerRB.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }


}

