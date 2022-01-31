using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private SliderJoint2D _slider;
    private Rigidbody2D rb;
    private float _speed;
    private float currentSpeed;

    void Start()
    {
        _slider = GetComponent<SliderJoint2D>();
        _speed = _slider.motor.motorSpeed;
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = rb.velocity.y;
    }

    void Update()
    {
        var motor = _slider.motor;
        motor.motorSpeed = _speed;
        _slider.motor = motor;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PL_stopper"))
           // if(currentSpeed == _speed)
                _speed *= -1;
    }
}
