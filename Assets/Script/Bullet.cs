using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
    IEnumerator MiCorutina()
    {
        myAnim.SetBool("isBulletnormal", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);


    }


    // Update is called once per frame
    void Update()
    {
        myRb.velocity = transform.right * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisionando : " + collision.gameObject.name);
        if (collision.gameObject)
        {
            
            StartCoroutine(MiCorutina());
           

        }
        
    }
}
