using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakableObject : MonoBehaviour

{
    public GameObject playerExpectedPosition;
    public float takableDistance;
    public string animationTrigger;
    public float animationDuration;

    private Hemophil_Player H_player;

    private void Start()
    {
        H_player = FindObjectOfType<Hemophil_Player>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerExpectedPosition.transform.position, H_player.transform.position);

        if (distance <= takableDistance && Input.GetButtonDown("Take"))
        {
            //TODO send message with all parameters
            H_player.TakingObject(playerExpectedPosition.transform, animationTrigger, gameObject, animationDuration, gameObject);
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawSphere(playerExpectedPosition.transform.position, takableDistance);
    }



}

