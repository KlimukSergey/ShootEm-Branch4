using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private  Score _score;
    

    void Awake()
    {
        _score = GameObject.Find("GameManager").GetComponent<Score>();
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 1200f);
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
        if (collision.gameObject.CompareTag("Enemy"))
        {
           _score.CountScore(1);
           collision.gameObject.GetComponent<EnemyAnimator>().Fail();
       collision.gameObject.GetComponent<EnemyContr>().AtHome();
           // Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<JanitorController>().TakeDamage(1);
        }
    }
}
