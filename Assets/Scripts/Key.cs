using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.transform.root.TryGetComponent<Player>(out var player))
        {
            player.keyNumber++;
            Destroy(gameObject);




        }
    }
}
