using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Collider activator;
    public Animator Doors;

    void OnTriggerEnter(Collider other)
    {
        if (other == activator)
        {
            Doors.SetBool("Doors",true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other == activator)
        {
            Doors.SetBool("close",false);
        }
    }
}
