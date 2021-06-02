using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Collider Hemophil_Player;
    private CameraManager camManager;
    public int cameraIdentity;

    private void Start()
    {
        camManager = FindObjectOfType<CameraManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other = Hemophil_Player)
        {
            camManager.SwitchCamera(cameraIdentity);
        }
    }









}
