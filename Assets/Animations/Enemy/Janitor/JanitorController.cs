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

    private const float STATE_Pursuit = 1,
        STATE_Attack = 2,
        STATE_Scream = 3,
        STATE_TakeDamage = 4;
    private float _currentState;

    private bool _noneDamage = false;
    private bool isAlive = true;
    private Text healthText;
    private float _speed;

    [SerializeField]
    private float _reload = 2f;

    private bool _attackSound = true;
    private bool _isAtack;
    private bool _isBroomKick;

    void Awake()
    {
        janitorAnim = GetComponentInChildren<JanitorAnim>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("PL_Target").GetComponent<Transform>();
        agent.destination = target.position;
        //   broomHit = GameObject.Find("BroomHit");
        healthText = GameObject.Find("BossHealthText").GetComponent<Text>();
        healthText.text = "";
        _speed = agent.speed;

        _isAtack = false;
        //  _currentState = STATE_Scream;
    }

    void FixedUpdate()
    {
        if (agent.enabled && Health.isAlive)
            //{
            agent.destination = target.position;
        //
        //          switch (_currentState)
        //        {
        //          case 1: // PURSUIT
        //            agent.destination = target.position;
        //          if (
        //            !janitorAnim.isAttackAnimation
        //          && agent.remainingDistance <= 3f
        //        && agent.remainingDistance != 0
        //  )
        //{
        //  if (!_isAtack)
        //    _currentState = STATE_Attack;
        //}
        //break;

        //case 2: // ATTACK
        //  transform.LookAt(target.position);

        // janitorAnim.Attack();

        //if (!janitorAnim.isAttackAnimation)
        // {
        //   string atackSoundName = $"Jan_attack_{Random.Range(1, 5)}";
        //  AudioManager.instance.Play_SFX(atackSoundName, this.transform);
        // }
        //_isAtack = true;
        //StartCoroutine(AttackPause());
        //_currentState = STATE_Pursuit;
        //break;

        // case 3: // /SCREAM
        //   janitorAnim.Scream();
        //  agent.speed = 0f;
        // StartCoroutine(ScreamTimer());
        //agent.destination = target.position;

        // _currentState = STATE_Pursuit;
        // break;

        //   case 4:
        //     ; // TakeDMG
        //   break;

        // default:
        //   break;
        // }
        /*             ; // Всегда движемся к Player

            if (agent.remainingDistance <= 3) // Если Player близко
            {
                ; // Смотреть на Player
                Attack();
            }
 */
        //}
    }
    //IEnumerator AttackPause()
    //{
    //   yield return new WaitForSeconds(3f);
    // _isAtack = false;
    //}
    //IEnumerator ScreamTimer()
    //{
    //  yield return new WaitForSeconds(3f);
    //agent.speed = _speed;

    //yield return new WaitForSeconds(Random.Range(10, 30));
    //if (_currentState == STATE_Pursuit)
    //   _currentState = STATE_Scream;
    // }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player" && !_isAtack)
        {
            StartCoroutine(StartAttack());
        }
    }
    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player" && _isBroomKick)
        {
            coll.GetComponent<Health>().TakeDamage(1);
            AudioManager.instance.Play_SFX("broomDamage", this.transform);
            _isBroomKick = false;
        }
    }
    public void Attack()
    {
        //   if (
        //   janitorAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("Standing Melee Attack Downward")
        //    )
        //    {
        //       if (!_isAtack)
        //          StartCoroutine(AnimTiming());
        //   }
        //   else
        //   {
        //       _isAtack = false;
        //       janitorAnim.Attack();
        //  }

        //  AttackVoice();
        //   if (
        //      janitorAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("Standing Melee Attack Downward")
        //   )
        //   {
        ///     Collider[] col = Physics.OverlapSphere(broomHit.transform.position, _range, HeadMask);

        //    foreach (Collider e in col)
        //     {
        //         print(e.gameObject.name);
        //         if (col != null)
        //         {
        //            e.gameObject.GetComponentInParent<Health>().TakeDamage(damage);
        //            AudioManager.instance.Play_SFX("broomDamage", this.transform);
        //        }
        //    }
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
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawSphere(broomHit.transform.position, 2f);
        // }
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
            string dmg_sound = $"Janitor_Dam_{Random.Range(1, 3)}";
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
        // janitorAnim.Death();
        Destroy(gameObject, 10f);
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
