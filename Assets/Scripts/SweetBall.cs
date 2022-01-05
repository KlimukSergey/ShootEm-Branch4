using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetBall : MonoBehaviour
{
    private Score _score;
    private Rigidbody rb;
    [SerializeField]
    GameObject sweetBallParticles;

    void Awake()
    {
        _score = GameObject.Find("GameManager").GetComponent<Score>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(SweetBallLife());
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * 20000f, ForceMode.Force);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (
            !collision.gameObject.CompareTag("Player")
            && !collision.gameObject.CompareTag("Ground")
            && !collision.gameObject.CompareTag("Enemy")
        )
        {
            DestroySweetBall();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
             AudioManager.instance.Play_SFX("SweetBall_Boy", this.transform);
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
    public void DestroySweetBall()
    {
        AudioManager.instance.Play_SFX("sweetballcrash",this.transform);

        GameObject parts = Instantiate(sweetBallParticles, transform.position, Quaternion.identity);
        Destroy(this.gameObject,1f);
        Destroy(parts, 1f);
    }
    IEnumerator SweetBallLife()
    {
        yield return new WaitForSeconds(10f);
        DestroySweetBall();
    }
}
