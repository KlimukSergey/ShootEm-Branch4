using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyContr : MonoBehaviour
{
    [SerializeField]
    private GameObject sweetPrefab;

    private NavMeshAgent agent;
    public Transform target;
    private Transform player;
    public Transform home;

    private const float Attach_STATE = 1,
        GoHome_STATE = 2;
    private float _currentState;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        target = player;
        agent.destination = target.position;
        _currentState = 1;
    }

    void Update()
    {
        switch (_currentState)
        {
            case 1: // Attach_State
                agent.destination = target.position;
                break;

            case 2:
                if (agent.remainingDistance < 1)
                {
                    FindObjectOfType<EneysSpawner>().enemies.Remove(gameObject);
                    Destroy(gameObject);
                }
                break;

            default:
                break;
        }
    }
    public void AtHome()
    {
        int drop = Random.Range(0, 3);
        if (drop == 1)
        {
            GameObject sweet = Instantiate(sweetPrefab, transform.position, transform.rotation);
            Destroy(sweet, 180f);
        }
        if (_currentState != 2)
        {
            _currentState = 0;
            agent.destination = home.position;
            agent.speed = 3f;
            agent.stoppingDistance = 1;
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        _currentState = 2;
    }
}
