using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorAnim : MonoBehaviour
{
    public Animator anim;
    JanitorController controller;
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponentInParent<JanitorController>();

    }

    // Update is called once per frame
    void Update()
    {
        float vlc = controller.agent.velocity.magnitude / 10;
        anim.SetFloat("Velocity", vlc);
        Scream();
    }
    public void Run()
    {

    }
    public void Attack()
    {
        anim.SetTrigger("Attack");

        //if(anim.GetCurrentAnimatorStateInfo(0).IsName("Standing Melee Attack Downward"))
        // {    
        //    controller.BroomAttack();
        //}


    }

    public void Hit()
    {
        print("POP");
        anim.SetTrigger("Hit");
    }
    public void Death()
    {
        anim.SetTrigger("Death");
    }
    void Scream()
    {
        anim.SetTrigger("Screamer");
        StartCoroutine(ScreamWait());
    }
    IEnumerator ScreamWait()
    {
        yield return new WaitForSeconds(Random.Range(20, 50));
        Scream();
    }
}
