using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{

void OnTriggerEnter(Collider coll)
{
   // if(coll.CompareTag("Broom"))
    {
       print (coll.name);
    }
}
}
