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
    public bool isMoveLeft, isDead;
    private GameObject gameController;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioEffect;
    public AudioClip jump, dead, breakBlock;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2.5f;
        force = 250;
        isGrounded = true;
        isDead = false;
        obj = gameObject;
        spriteRenderer = obj.GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = true; // turn right character
        anim = obj.GetComponent<Animator>();
        anim.SetBool("isDead", false);
        anim.SetBool("isGround", true);
        anim.SetBool("isMoving", false);
        anim.SetBool("isFly", false);
        gameController = GameObject.FindGameObjectWithTag("GameController");
        audioEffect = gameObject.AddComponent<AudioSource>();
        audioEffect.playOnAwake = false;
        audioEffect.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapArea(topLeft.position, bottomRight.position, groundLayer);
        anim.SetBool("isGround", isGrounded);
        if(isGrounded)
        {
            anim.SetBool("isFly", false);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            obj.transform.Translate(new Vector3(1 * speed * Time.deltaTime, 0, 0));
            isMoveLeft = false;
            anim.SetBool("isMoving", true);
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            obj.transform.Translate(new Vector3(-1 * speed * Time.deltaTime, 0, 0));
            isMoveLeft = true;
            anim.SetBool("isMoving", true);
            spriteRenderer.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force));
            anim.SetBool("isFly", true);
            audioEffect.clip = jump;
            audioEffect.Play();
        }
        if(!Input.anyKey)
        {
            anim.SetBool("isMoving", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            if (transform.position.y - collision.gameObject.transform.position.y > 0.4f)
            {
                collision.gameObject.GetComponent<EnemyController>().isDeath = true;
            }
            else
            {
                anim.SetBool("isDead", true);
                speed = 0;
                gameController.GetComponent<GameController>().isLoose = true;
                audioEffect.clip = dead;
                audioEffect.Play();
            }
        }
        else if(collision.collider.tag=="Block")
        {
            if (transform.position.y - collision.gameObject.transform.position.y < -0.45f)
            {
                audioEffect.clip = breakBlock;
                audioEffect.Play();
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Flag")
        {
            gameController.GetComponent<GameController>().isWin = true;
        }
    }
}
