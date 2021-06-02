using UnityEngine;
using System.Collections;

public class Doors_OpeningAndClose : MonoBehaviour
{
    [Header("meme fonctionnement que la porte avec parametre trigger: portal_Activation ET portal_Deactivation")]
    public Collider activator;
    public Animator Doors;


void OnTriggerEnter(Collider other)
{
    if (other == activator)
    {
         Doors.SetTrigger("portal_Activation");
    }
}

void OnTriggerExit(Collider other)
{
    if (other == activator)
    {
         Doors.SetTrigger("portal_Deactivation");
    }
}

}