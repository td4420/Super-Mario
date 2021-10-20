using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float distance = 1;
    public float speed = 2;
    float minX, maxX;
    bool isMoveLeft;
    public bool isDeath;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        isDeath = false;
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isDeath", false);
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
        if(!isDeath)
        {
            if (isMoveLeft)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x - speed * Time.deltaTime, minX, maxX), transform.position.y, transform.position.z);
            }
            else transform.position = new Vector3(Mathf.Clamp(transform.position.x + speed * Time.deltaTime, minX, maxX), transform.position.y, transform.position.z);
        }
        else
        {
            animator.SetBool("isDeath", true);
            Destroy(gameObject,0.3f);
        }
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
