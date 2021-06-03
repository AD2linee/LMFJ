using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hemophil_Player : MonoBehaviour
{
    public Camera Cam;
    public float moveSpeed;
    private Vector3 moveAxes;

    private CharacterController controller;
    private Animator animator;
    private float deadZone = 0.2f;

    private float horizAxis;
    private float vertAxis;

    [Header("test deplacement auto")]
    private bool loosedControl;

    private Transform expectedPositionToTake;
    public float tolerance;
    public float toleranceAngle;
    private GameObject objectToTake;
    private GameObject ObjectTolookAtDuringTurn;

    private float animationDuration;
    private GameObject objectToDestroy;

    private float _playerDir;
    private float _expectedDir;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        ObjectTolookAtDuringTurn = new GameObject("Object to look at during turn");
    }
    private void Update()
    {
        // ---- gravity --------
        Vector3 gravity = Vector3.up * -9.81f;
        controller.Move(gravity);
        //----------------------

        if (!loosedControl)
        {
            //--- input managment ---
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > deadZone)
            {
                horizAxis = Input.GetAxis("Horizontal");
            }
            else { horizAxis = 0; }


            if (Mathf.Abs(Input.GetAxis("Vertical")) > deadZone)
            {
                vertAxis = Input.GetAxis("Vertical");
            }
            else { vertAxis = 0; }
            //----------------------

            if (vertAxis != 0 || horizAxis != 0)
            {
                //------ movment -------     depending of camera.rotation
                Vector3 camF = Cam.transform.forward;
                Vector3 camR = Cam.transform.right;
                camF.y = 0;
                camR.y = 0;
                camF = camF.normalized;
                camR = camR.normalized;
                Vector3 dir = camF * vertAxis + camR * horizAxis;
                moveAxes = dir;

                controller.Move(dir * moveSpeed * Time.deltaTime);
                //----------------------

                transform.rotation = Quaternion.LookRotation(dir);
            }

            if ((Mathf.Abs(controller.velocity.x) > 0 || Mathf.Abs(controller.velocity.z) > 0) && !animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
            { 
                animator.SetTrigger("walk");
            }
            if ((controller.velocity.x == 0 || controller.velocity.z == 0) && !animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                animator.SetTrigger("idle");
            }
        }
        else //---------------------- auto move to take an object
        {
            Vector3 offset = expectedPositionToTake.position - transform.position;
            offset.y = 0;
            Quaternion playerDir = transform.rotation;
            Quaternion expectedDir = expectedPositionToTake.rotation;

            Vector3 dist = ObjectTolookAtDuringTurn.transform.position - objectToTake.transform.position;
            dist.y = 0;
            float distance = dist.magnitude;

            if (offset.magnitude > tolerance)
            {
                offset = offset.normalized * moveSpeed;
                if (controller.velocity.magnitude == 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
                {
                    animator.SetTrigger("walk");
                }
                controller.Move(offset * Time.deltaTime);
                transform.LookAt(expectedPositionToTake, Vector3.up);

                Vector3 pos = new Vector3(transform.localPosition.x, transform.position.y, transform.localPosition.z);
                ObjectTolookAtDuringTurn.transform.position = pos;
                ObjectTolookAtDuringTurn.transform.rotation = transform.rotation;
                ObjectTolookAtDuringTurn.transform.Translate(Vector3.forward * 1, Space.Self);
                Debug.Log("1 : deplacement jusqu'a la cible");
            }
            else if (distance >= toleranceAngle)
            {
                Vector3 posToWatch = new Vector3( objectToTake.transform.position.x, transform.position.y, objectToTake.transform.position.z);
                ObjectTolookAtDuringTurn.transform.position = Vector3.Lerp(ObjectTolookAtDuringTurn.transform.position, posToWatch, 0.8f);
                transform.LookAt(ObjectTolookAtDuringTurn.transform, Vector3.up);

                Debug.Log("2 : rotation en direction de la cible");


            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("take"))
            {
                animator.SetTrigger("take");
                StartCoroutine(TakeAnimationTimer(animationDuration));
                Debug.Log("3 : animation, destruction apres delai");

            }

        }
    }

    IEnumerator TakeAnimationTimer(float time)
    {
        yield return new WaitForSeconds(time);
        loosedControl = false;
    }

    public void TakingObject(Transform expectedPosition, string animationTrigger, GameObject objToTake, float animDuration, GameObject _objectToDestroy)
    {
        loosedControl = true;
        expectedPositionToTake = expectedPosition;
        objectToTake = objToTake;
        animationDuration = animDuration;
        objectToDestroy = _objectToDestroy;
    }

    public void DestroyObjectToTake()
    {
        Destroy(objectToDestroy);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + moveAxes);

        if (ObjectTolookAtDuringTurn != null)
        {
            Gizmos.DrawSphere(ObjectTolookAtDuringTurn.transform.position, 0.2f);
        }
    }


















}