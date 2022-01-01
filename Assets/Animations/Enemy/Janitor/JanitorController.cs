using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class JanitorController : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform target;
    JanitorAnim janitorAnim;

    [SerializeField]
    private GameObject broomHit;

    [SerializeField]
    private LayerMask HeadMask;

    [SerializeField]
    private int damage = 1;

    [SerializeField]
    private int currentHealth = 10;

    [SerializeField]
    GameObject broom;

    private bool _noneDamage = false;
    private bool isAlive = true;
    private Text healthText;
    private float _speed;

    [SerializeField]
    private float _range = 2f;

    void Awake()
    {
        janitorAnim = GetComponentInChildren<JanitorAnim>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        agent.destination = target.position;
        //   broomHit = GameObject.Find("BroomHit");
        healthText = GameObject.Find("BossHealthText").GetComponent<Text>();
        healthText.text = /* $"Health: {currentHealth.ToString()}"*/
            "";
        _speed = agent.speed;
    }

    void Update()
    {
        if (agent.enabled)
        {
            agent.destination = target.position;

            if (agent.remainingDistance <= 3)
            {
                transform.LookAt(target.position);
                //              if (janitorAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("Standing Melee Attack Downward"))

                Attack();
            }
        }
    }

    public void Attack()
    {
        janitorAnim.Attack();

        if (
            janitorAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("Standing Melee Attack Downward")
        )
        {
            //  broomHit.transform.LookAt(target.position);
            //   Ray ray = new Ray(broomHit.transform.position, target.transform.position);

            //  if (Physics.Raycast(
            //broomHit.transform.position,
            //broomHit.transform.forward,
            //        ray,
            //        1f, HeadMask))
            //   {
            //      Debug.DrawRay(broomHit.transform.position,
            //       broomHit.transform.forward, Color.red, 2f);
            Collider[] col = Physics.OverlapSphere(broomHit.transform.position, _range, HeadMask);
            //     Gizmos.color = Color.red;
            //     Gizmos.DrawSphere(broomHit.transform.position, 2f);
            foreach (Collider e in col)
            {
                if (col != null)
                {
                    e.gameObject.GetComponentInParent<Health>().TakeDamage(damage);
                    AudioManager.instance.Play_SFX("broomDamage", this.transform);
                }
            }
        }
    }
    public void TakeDamage(int dmg)
    {
        if (!_noneDamage && isAlive)
        {
            string dmg_sound = $"Janitor_Dam_{Random.Range(1,3)}";
            AudioManager.instance.Play_SFX(dmg_sound, this.transform);
            janitorAnim.Hit();

            agent.speed = 0f;
            StartCoroutine(MoveStop());
            StartCoroutine(NonDamage());
            _noneDamage = true;

            currentHealth -= dmg;
            healthText.text = $"Health: {currentHealth.ToString()}";

            if (currentHealth <= 0)
                Death();
        }
    }

    void Death()
    {
        AudioManager.instance.Play_SFX("Janitor_Die", this.transform);
        healthText.text = "";
        isAlive = false;
        _noneDamage = true;
        currentHealth = 0;
        StopCoroutine(NonDamage());
        StartCoroutine(Broom());
        this.agent.enabled = false;
        janitorAnim.Death();
        Destroy(gameObject, 15f);
        FindObjectOfType<Score>().CountScore(20);
        FindObjectOfType<EneysSpawner>().AgainSpawn(); // спавн Карапузов
    }
    IEnumerator NonDamage()
    {
        yield return new WaitForSeconds(3f);
        _noneDamage = false;
    }
    IEnumerator MoveStop()
    {
        yield return new WaitForSeconds(1f);
        agent.speed = _speed;
    }
    IEnumerator Broom()
    {
        yield return new WaitForSeconds(0.6f);
        broom.AddComponent<Rigidbody>().AddForce(transform.right * 200);
    }
}
