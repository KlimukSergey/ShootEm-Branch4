using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
