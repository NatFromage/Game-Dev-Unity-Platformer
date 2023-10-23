using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Spikes : MonoBehaviour
{
    [SerializeField] DamageController damageController;
    [SerializeField] private int damage;


    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            damageController.Damage(damage);
        }
    }
    // Update is called once per frame
}
