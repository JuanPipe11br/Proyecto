using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class volador : MonoBehaviour
{

    [SerializeField] GameObject player;
    AIPath myPhath;
    // Start is called before the first frame update
    void Start()
    {
        myPhath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        Perseguir();


    }
    void Perseguir()
    {
        float d = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log("Distancia" + d);
        if (d < 8)
        {

        }
        Debug.DrawLine(transform.position, player.transform.position, Color.red);
        ///Radar
         Collider2D col= Physics2D.OverlapCircle(transform.position, 5f, LayerMask.GetMask("Player"));
        if (col != null)
        {
            myPhath.isStopped = false;
        }
        else
        {
            myPhath.isStopped = true;

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
