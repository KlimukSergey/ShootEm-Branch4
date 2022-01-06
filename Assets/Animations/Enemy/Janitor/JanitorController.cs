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
    private float _reload = 2f;

    private bool _isAtack;
    private bool _isBroomKick;
    private bool _isDamage;

    void Awake()
    {
        janitorAnim = GetComponentInChildren<JanitorAnim>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("PL_Target").GetComponent<Transform>();
        agent.destination = target.position;
        healthText = GameObject.Find("BossHealthText").GetComponent<Text>();
        healthText.text = "";
        _speed = agent.speed;

        _isAtack = false;
        _isDamage = false;

        StartCoroutine(Scream());
    }

    void FixedUpdate()
    {
        if (agent.enabled && Health.isAlive)
        {
            agent.destination = target.position;
            if (agent.remainingDistance <= 4)
                transform.LookAt(target.position);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (isAlive && coll.tag == "Player" && Health.isAlive)
        {
            if (!_isAtack && !_isDamage)
            {
                StartCoroutine(StartAttack());
            }
        }
    }
    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player" && _isBroomKick)
        {
            coll.GetComponent<Health>().TakeDamage(damage);
            AudioManager.instance.Play_SFX("broomDamage", this.transform);
            _isBroomKick = false;
        }
    }

    IEnumerator StartAttack()
    {
        _isAtack = true;

        janitorAnim.Attack();

        yield return new WaitForSeconds(0.2f);
        string atackSoundName = $"Jan_attack_{Random.Range(1, 5)}";
        AudioManager.instance.Play_SFX(atackSoundName, this.transform);

        yield return new WaitForSeconds(0.15f);
        _isBroomKick = true;

        yield return new WaitForSeconds(0.1f);
        _isBroomKick = false;

        yield return new WaitForSeconds(_reload);
        _isAtack = false;
    }

    public void TakeDamage(int dmg)
    {
        if (!_noneDamage && isAlive)
        {
            _isDamage = true;
            string dmg_sound = $"Janitor_Dam_{Random.Range(1, 3)}";
            AudioManager.instance.Play_SFX(dmg_sound, this.transform);
            janitorAnim.Hit();

            agent.speed = 0f;
            StartCoroutine(Damage());
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
        StopCoroutine(Damage());
        StartCoroutine(Broom());
        this.agent.enabled = false;
        janitorAnim.Death();
        Destroy(gameObject, 10f);
        FindObjectOfType<Score>().CountScore(20);
        FindObjectOfType<EneysSpawner>().AgainSpawn(); // спавн Карапузов
    }
    IEnumerator Broom()
    {
        yield return new WaitForSeconds(0.6f);
        broom.AddComponent<Rigidbody>().AddForce(transform.right * 200);
    }

    IEnumerator Damage()
    {
        yield return new WaitForSeconds(1f);
        agent.speed = _speed;
        _isDamage = false;

        yield return new WaitForSeconds(2f);
        _noneDamage = false;
    }
    IEnumerator Scream()
    {
        if (isAlive && Health.isAlive)
        {
            if (!_isAtack && !_isDamage)
            {
                janitorAnim.Scream();
                yield return new WaitForSeconds(0.2f);
                AudioManager.instance.Play_SFX("Janitor_Scream", this.transform);
                agent.speed = 0;
                yield return new WaitForSeconds(2);
                agent.speed = _speed;
            }
        }
        yield return new WaitForSeconds(Random.Range(20, 40));
        StartCoroutine(Scream());
    }
}
