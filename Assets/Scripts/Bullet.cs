using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private Score _score;
    private Rigidbody rb;

    [SerializeField]
    GameObject snowBallParticles;

    void Awake()
    {
        _score = GameObject.Find("GameManager").GetComponent<Score>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 1200f);
        StartCoroutine(SnowBallLife());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.Play_SFX("snowball", this.transform);
            DestroySnowBall();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioManager.instance.Play_SFX("body", this.transform);
            AudioManager.instance.Play_SFX("BoyDam", this.transform);

            _score.CountScore(1);
            collision.gameObject.GetComponent<EnemyAnimator>().Fail();
            collision.gameObject.GetComponent<EnemyContr>().AtHome();
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            AudioManager.instance.Play_SFX("body", this.transform);

            collision.gameObject.GetComponent<JanitorController>().TakeDamage(1);
        }
    }

    public void DestroySnowBall()
    {
        GameObject parts = Instantiate(snowBallParticles, transform.position, Quaternion.identity);
        Destroy(GetComponent<Renderer>());
        Destroy(GetComponent<SphereCollider>());
        Destroy(this.gameObject, 1f);
        Destroy(parts, 1f);
    }
    
    IEnumerator SnowBallLife()
    {
        yield return new WaitForSeconds(10f);
        DestroySnowBall();
    }
}
