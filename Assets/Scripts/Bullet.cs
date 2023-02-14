using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D myRigidbody;
    PlayerMovement player;
    float xSpeed;
  
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }


    void Update()
    {
        myRigidbody.velocity = new Vector2(xSpeed,0f);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag =="BulletRange")
        {
            Destroy(gameObject);
        }

    }
}
