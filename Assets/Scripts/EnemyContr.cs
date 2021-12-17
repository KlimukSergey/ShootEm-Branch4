using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyContr : MonoBehaviour
{
    [SerializeField] private float _damage = 1f;
    private NavMeshAgent agent;
    private Transform target;
    private Transform player;
     public Transform home;
     private const float 
     Attach_STATE=1,
     GoHome_STATE=2;
private float _currentState;

    void Awake()
    {
              agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        target = player;
                agent.destination = target.position;
                _currentState=1;
       
    }

    // Update is called once per frame
    void Update()
    {
               switch(_currentState)
        {
            case 1:
         agent.destination = target.position;

                break;
                case 2:
                if(agent.remainingDistance<=1 && agent.destination==home.position)
                Destroy(gameObject);
                break;
                default :break;
        }
    }
    public void AtHome()
    {
        if(_currentState!=2)
        {
        _currentState=2;
        agent.speed = 3f;
        agent.destination = home.position;
        agent.stoppingDistance = 0;
        }
    }


}
