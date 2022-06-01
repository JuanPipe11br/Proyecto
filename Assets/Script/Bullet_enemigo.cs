using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_enemigo : MonoBehaviour
{
    private Rigidbody2D myRb;
    [SerializeField] float speed;
    Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()

    {
        myRb.velocity = transform.right * speed;
        myAnim.SetBool("isBala", true);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisionando : " + collision.gameObject.name);
        Destroy(this.gameObject);
    }
}
