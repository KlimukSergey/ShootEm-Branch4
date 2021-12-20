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
    private GameObject broomHit;
    [SerializeField]
    private LayerMask playerMask;
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

    void Awake()
    {
        janitorAnim = GetComponentInChildren<JanitorAnim>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        agent.destination = target.position;
        broomHit = GameObject.Find("BroomHit");
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

            if (agent.remainingDistance <= 4)
            {
                transform.LookAt(target.position);
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
            broomHit.transform.LookAt(target.position);
            Ray ray = new Ray(broomHit.transform.position, target.transform.position);
            if (
                Physics.Raycast(
                    broomHit.transform.position,
                    broomHit.transform.forward,
                    2f,
                    playerMask
                )
            )
            {
                target.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    public void TakeDamage(int dmg)
    {
        if (!_noneDamage && isAlive)
        {
            janitorAnim.Hit();
            agent.speed = 0f;
            currentHealth -= dmg;
            _noneDamage = true;
            healthText.text = $"Health: {currentHealth.ToString()}";
            StartCoroutine(NonDamage());

            if (currentHealth <= 0)   Death();
                
        }
    }

    void Death()
    {
        healthText.text = "";
        isAlive = false;
        _noneDamage = true;
        currentHealth = 0;
        StopCoroutine(NonDamage());
        StartCoroutine(Broom());
        agent.enabled = false;
        janitorAnim.Death();
        Destroy(gameObject, 15f);
        FindObjectOfType<Score>().CountScore(20);
        FindObjectOfType<EneysSpawner>().AgainSpawn(); // спавн Карапузов
    }
    IEnumerator NonDamage()
    {
        yield return new WaitForSeconds(3f);
        _noneDamage = false;
        agent.speed = _speed;
    }
    IEnumerator Broom()
    {
        yield return new WaitForSeconds(2f);
        broom.AddComponent<Rigidbody>().AddForce(transform.right * 200);
    }
}
