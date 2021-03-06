using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private GameManager manager;
    Shooting shooting;
    private float _currentSpeed;
    private float _slowSpeed;
    private CharacterController controller;
    private Vector3 moveVector;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        shooting = FindObjectOfType<Shooting>();
        _currentSpeed = speed;
        _slowSpeed = speed - 3f;
    }
    private void Update()
    {
       // AIM();

        if (!controller.isGrounded)
            controller.Move(Vector3.down * 10f);

        if (controller.transform.rotation.x != 0)
        {
            Vector3 vec = new Vector3(controller.transform.rotation.x, 0f, 0f);
            controller.Move(vec * -1);
        }
        if(Input.GetMouseButtonUp(1))speed=_currentSpeed;
    }
    /// <summary>
    /// прицеливание   
    /// </summary>
    public void AIM()
    {
            speed = _slowSpeed;
            float hit;
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            if (plane.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                transform.LookAt(Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(hit));
            }
    }

    public void Move(float x, float y)
    {
        moveVector.x = x * speed;
        moveVector.z = y * speed;
        controller.Move(moveVector * Time.deltaTime);

        if (
            Vector3.Angle(Vector3.forward, moveVector) > 1f
            || Vector3.Angle(Vector3.forward, moveVector) == 0
        )
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speed, 0.0f);
            if (!Input.GetMouseButton(1))
                transform.rotation = Quaternion.LookRotation(direct);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("bulletDrop"))
        {
            AudioManager.instance.Play_SFX("ammo", this.transform);
            AudioManager.instance.Play_SFX("collect", this.transform);
            Destroy(col.gameObject);
            shooting.CollectBullet(5);
        }
        if (col.CompareTag("sweet"))
        {
            Destroy(col.gameObject);
            Score.sweetCount++;
            AudioManager.instance.Play_SFX("collect", this.transform);
        }
    }
}
