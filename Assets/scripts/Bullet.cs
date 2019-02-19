using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 20f;
    public Rigidbody2D Rb;
    public int damage = 1;

    // Use this for initialization
    void Start()
    {
        Rb.velocity = transform.right* speed;

    }

     void OnTriggerEnter2D(Collider2D hitInfo)
     {
        Player player = hitInfo.GetComponent<Player>();
        if (player != null)
        {
        
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
     
     }
}
