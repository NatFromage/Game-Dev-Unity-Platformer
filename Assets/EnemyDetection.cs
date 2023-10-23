using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{

    Collider2D coll;
    public List<Collider2D> detectedColls = new List<Collider2D>();


    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColls.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColls.Remove(collision);
            
    }
}
