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
            Debug.Log(Doors);
            Doors.SetBool("Open", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other == activator)
        {
            Doors.SetBool("Open",false);
        }
    }
}
