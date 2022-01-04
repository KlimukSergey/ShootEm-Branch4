using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomAttack : MonoBehaviour
{
     void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag=="Player"&&JanitorAnim.broomKick)
        {
print ("BroomContact!");
other.GetComponent<Health>().TakeDamage(1);
        }
    }
}
