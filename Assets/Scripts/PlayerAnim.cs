using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator anim;
    private CharacterController player;


    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        anim.SetFloat("velocity",player.velocity.magnitude);
    }
    public void Laser(bool active)
    {
        anim.SetBool("laser",active);
    }
    public void Run(bool active)
    {
        anim.SetBool("run",active);
    }
    public void Death()
    {
        anim.SetTrigger("death");
    }
}
