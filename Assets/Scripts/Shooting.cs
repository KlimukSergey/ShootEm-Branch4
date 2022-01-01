using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject sweetBallPrefab;
    PlayerAnim animator;
    public int bulletCount = 20;
    private Text ui;
    private RaycastHit hit;
    private Ray ray;
    private LineRenderer line;
    private Transform lineTarget;
    private Vector3 target;

    private const float State_SHOOT = 1,
        State_LASER = 2;
    private float _currentState;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        lineTarget = GameObject.Find("AimPoint").transform;
        ui = GameObject.Find("BulletCount").GetComponent<Text>();
        ui.text = bulletCount.ToString();
        animator = GameObject.Find("PlayerBody").GetComponent<PlayerAnim>();
    }

    void Update()
    {
        if (Time.timeScale != 0&&Health.isAlive)
        {
            if (Input.GetMouseButton(1))
            {
                animator.Laser(true);
                line.enabled = true;
                _currentState = State_LASER;
            }
            if (Input.GetMouseButtonUp(1))
            {
                animator.Laser(false);
                _currentState = 0;
                line.enabled = false;
            }
            if (Input.GetMouseButtonDown(0))
                _currentState = State_SHOOT;

            switch (_currentState)
            {
                case 1: // Shoot
                    if (bulletCount > 0)
                    {
                        AudioManager.instance.Play_SFX("shoot", this.transform);

                        bulletCount--;
                        
                        ui.text = bulletCount.ToString();

                        GameObject bullet = Instantiate(
                            bulletPrefab,
                            transform.position,
                            transform.rotation         );
                    }
                    else
                    {
                        AudioManager.instance.Play_SFX("empty", this.transform);
                    }
                    _currentState = 0;
                    break;

                case 2: // Laser
                    Laser();
                    break;
                default:
                    break;
            }
        }
    }

    private void Laser()
    {
        ray = new Ray(transform.position, transform.forward * 20f);
        if (Physics.Raycast(ray, out hit))
        {
            target = hit.point;
        }
        else
            target = lineTarget.position;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, target);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("bulletDrop"))
        {
            Destroy(col.gameObject);
        }
    }
    public void CollectBullet(int count)
    {
        bulletCount += count;
        ui.text = bulletCount.ToString();
    }
    public void SuperShoot()
    {
        if (Score.sweetCount >= 10)
        {
            GameObject ball = Instantiate(sweetBallPrefab, transform.position, transform.rotation);
            Score.sweetCount -= 10;
        }
    }
}
