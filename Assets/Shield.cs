using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    [SerializeField]
    private HealthSystem healthSystem;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            Debug.Log("player hit shield");
            healthSystem.playerHealth += 1;
            healthSystem.UpdateHealth();

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }

        
    }

    // Update is called once per frame

}
