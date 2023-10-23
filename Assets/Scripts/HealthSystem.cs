using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int playerHealth;

    [SerializeField] private Image[] shields;
    [SerializeField] private PlayerController player;


    private void Awake()
    {
  
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        if(playerHealth <=0)
        {
            player.anim.SetBool("IsDead", true);
            player.theRB.bodyType = RigidbodyType2D.Static;
            player.capsuleCollider2D.enabled = false;
            player.enabled = false;
        }

    for(int i = 0;i<shields.Length;i++)
        {
            if (i < playerHealth)
            {
                            
                    shields[i].color = Color.white;
                }
            else
            {
                shields[i].color = Color.grey;
            }
        }
    }    

}
