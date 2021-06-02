using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] CamPositions;
    public Camera cam;
    public GameObject Hemophil_Player;

    public void SwitchCamera(int identity)
    {
        if(cam.gameObject.transform.position != CamPositions[identity].transform.position)
        {
            cam.gameObject.transform.position = CamPositions[identity].transform.position;
            cam.gameObject.transform.rotation = CamPositions[identity].transform.rotation;
        }
    }
}
