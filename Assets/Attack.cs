using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private PolygonCollider2D attackCollider;



    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.CompareTag("Enemy"))
        { 
            Debug.Log("ENEMY HIT");
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }      
    }

}
