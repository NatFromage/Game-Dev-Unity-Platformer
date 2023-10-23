using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private PolygonCollider2D attackCollider;

   private DamageController doDamage;


    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER HIT");
            player.GetComponent<DamageController>().Damage(attackDamage);
        }
    }
}
