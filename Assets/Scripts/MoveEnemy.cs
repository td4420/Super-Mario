using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public float distance = 1;
    public float speed = 2;
    float minX, maxX;
    bool isMoveLeft;
    // Start is called before the first frame update
    void Start()
    {
        minX = transform.position.x - distance;
        maxX = transform.position.x + distance;
       isMoveLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x == minX)
        {
            isMoveLeft = false;
        }
        if(transform.position.x == maxX)
        {
            isMoveLeft = true;
        }
        if (isMoveLeft)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x - speed*Time.deltaTime, minX, maxX), transform.position.y, transform.position.z);
        }
        else transform.position = new Vector3(Mathf.Clamp(transform.position.x + speed*Time.deltaTime, minX, maxX), transform.position.y, transform.position.z);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Enemy")
        {
            collision.collider.isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Enemy")
        {
            collision.isTrigger = false;
        }
    }
}
