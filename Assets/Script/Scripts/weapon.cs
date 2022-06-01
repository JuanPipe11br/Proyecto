using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject balaPrefab;
    private Transform _firePoint;

    void Awake()
    {
        _firePoint = transform.Find("FirePoint");
    }
    void Start()
    {
       GameObject obj= Instantiate(balaPrefab, _firePoint.position, Quaternion.identity)as GameObject ;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
