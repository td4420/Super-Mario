using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    private GameObject obj;
    private bool isGrounded;
    public float speed, force;
    public Transform topLeft, bottomRight;
    public LayerMask groundLayer;
    public bool isMoveLeft;
    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        obj = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapArea(topLeft.position, bottomRight.position, groundLayer);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            obj.transform.Translate(new Vector3(1 * speed * Time.deltaTime, 0, 0));
            isMoveLeft = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            obj.transform.Translate(new Vector3(-1 * speed * Time.deltaTime, 0, 0));
            isMoveLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            if (transform.position.y - collision.gameObject.transform.position.y > 0.5f)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("Die");
            }
        }
        else if(collision.collider.tag=="Block")
        {
            if (transform.position.y - collision.gameObject.transform.position.y > 0.5f)
            {
                Debug.Log("Stand On");
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Flag")
        {
            Debug.Log("You Win");
        }
    }
}
