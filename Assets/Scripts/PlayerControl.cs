using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private RaycastHit hit;
    private Transform weaponPoint;
    private LineRenderer lineRenderer;
    Shooting shooting;

    
    void Start()
    {
        lineRenderer = GameObject.Find("Line").GetComponent<LineRenderer>();
        weaponPoint = GameObject.Find("Sphere").transform;
        lineRenderer.enabled = false;
        shooting = FindObjectOfType<Shooting>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mousePos = ray.direction;
        transform.LookAt(mousePos);
        if (transform.position.y != 0) transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        if (Input.GetMouseButton(1))
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, weaponPoint.position);
            lineRenderer.SetPosition(1, transform.forward * 20f);

            //  Debug.DrawRay(weaponPoint.position,transform.forward*10f,Color.black);
        }
        if (Input.GetMouseButtonUp(1)) lineRenderer.enabled = false;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("bulletDrop"))
        {
            print("CollectionBase");
            Destroy(col.gameObject);
            shooting.CollectBullet(5);
        }
    }

}
