using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorAnim : MonoBehaviour
{
    public Animator anim;
    JanitorController controller;
    private float _agentSpeed;

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponentInParent<JanitorController>();
        _agentSpeed = controller.agent.speed;
    }

    void Update()
    {
        float vlc = controller.agent.velocity.magnitude / 10;
        anim.SetFloat("Velocity", vlc);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void Hit()
    {
        anim.SetTrigger("Hit");
    }

    public void Death()
    {
        anim.SetTrigger("Death");
    }

    public void Scream()
    {
        anim.SetTrigger("Scream");
    }
}
