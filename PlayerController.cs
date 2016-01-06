using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


    public float moveSpeed = 6f;
    public WeaponMove weapon;

    public enum PlayerState { Moving, Attacking, Building };
 
    [HideInInspector]
    public PlayerState currentState;

    Rigidbody rigidbody;
    CameraControl mainCam;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        mainCam = FindObjectOfType<CameraControl>();


        currentState = PlayerState.Moving;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && currentState == PlayerState.Moving)
        {
            currentState = PlayerState.Attacking;

            StartCoroutine(weapon.SwingWeapon());
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if(mainCam.currentType == CameraControl.CameraMode.Orthographic)
            {
                mainCam.ChangeProjectionType(CameraControl.CameraMode.Perspective);

                StartCoroutine(Fading.fading.Fade());

                currentState = PlayerState.Moving;
            }
            else if (mainCam.currentType == CameraControl.CameraMode.Perspective)
            {
                mainCam.ChangeProjectionType(CameraControl.CameraMode.Orthographic);

                StartCoroutine(Fading.fading.Fade());

                currentState = PlayerState.Building;
            }
        }
    }

    void FixedUpdate()
    {
        if(currentState == PlayerState.Moving)
            Move();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        movement = movement.normalized;

        rigidbody.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

        transform.LookAt(transform.position + movement);

    }
}
