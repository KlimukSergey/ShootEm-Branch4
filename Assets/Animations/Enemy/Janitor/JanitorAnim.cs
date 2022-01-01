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
        Scream();
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

    void Scream()
    {
        AudioManager.instance.Play_SFX("Janitor_Scream", this.transform);
        
        anim.SetTrigger("Scream");
        controller.agent.speed = 0f;
        StartCoroutine(StopMove());
        StartCoroutine(ScreamWait());
    }

    IEnumerator ScreamWait()
    {
        yield return new WaitForSeconds(Random.Range(20, 50));
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Standing Melee Attack Downward"))
            Scream();
    }
    
    IEnumerator StopMove()
    {
        yield return new WaitForSeconds(2.2f);
        controller.agent.speed = _agentSpeed;
    }
}
