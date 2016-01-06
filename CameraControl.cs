using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public Transform target;

    public enum CameraMode { Orthographic, Perspective };

    public CameraMode currentType;

    Camera cam;

    [Range(0, 1)]
    public float smoothing = 0.5f;

    void Start()
    {
        cam = GetComponent<Camera>();

        currentType = CameraMode.Perspective;

        cam.fieldOfView = 90;
    }

    void Update()
    {
        if(target != null && currentType == CameraMode.Perspective)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + new Vector3(0, 10, -12), smoothing * Time.deltaTime);
        }
    }

    public void ChangeProjectionType(CameraMode mode)
    {
        CameraMode state = mode;
        

        switch(state)
        {
            case CameraMode.Perspective:
                cam.orthographic = false;

                transform.Rotate(new Vector3(-45, 0, 0));

                break;

            case CameraMode.Orthographic:
                cam.orthographic = true;
                cam.transform.position = new Vector3(0, 20, 0);
                cam.orthographicSize = 25;

                transform.LookAt(Vector3.zero);

                break;
        }

        currentType = state;
    }
}
