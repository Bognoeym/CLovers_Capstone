using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public float Mouse_rot_speed = 5.0f;

    public Camera MainCamera;
    public GameObject VerticalGimbal;

    PlayerController player;

    float CameraMaxDistance = 2f;
    float camera_pitch;
    float mouseX;
    float mouseY;
    RaycastHit hit;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, MainCamera.transform.position - transform.position, Color.red);
        Debug.DrawRay(transform.position, (MainCamera.transform.position - transform.position).normalized * CameraMaxDistance, Color.blue);

        if (Physics.Raycast(transform.position, (MainCamera.transform.position - transform.position).normalized, out hit, CameraMaxDistance))
        {
            if (hit.transform.gameObject.tag != "Player")
            {
                MainCamera.transform.localPosition = Vector3.Lerp(MainCamera.transform.localPosition, 
                    MainCamera.transform.localPosition + Vector3.forward, Time.deltaTime * 10);
                MainCamera.transform.position = hit.point;
            }
        }
        else
        {
            MainCamera.transform.localPosition = Vector3.Lerp(MainCamera.transform.localPosition, new Vector3(0, 0, -CameraMaxDistance), Time.deltaTime * 5f);
            //Debug.DrawRay(transform.position, MainCamera.transform.position, Color.red);        
        }

        if (Input.GetMouseButton(1))
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            camera_pitch = transform.eulerAngles.x > 180 ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;


            transform.Rotate(Vector3.up * mouseX * Mouse_rot_speed, Space.World);
            if (camera_pitch <= 30 && camera_pitch >= -30)
            {
                VerticalGimbal.transform.Rotate(Vector3.left * mouseY * Mouse_rot_speed, Space.Self);
                camera_pitch = VerticalGimbal.transform.eulerAngles.x > 180 ? VerticalGimbal.transform.eulerAngles.x - 360 : VerticalGimbal.transform.eulerAngles.x;

                if (camera_pitch > 30)
                {
                    VerticalGimbal.transform.rotation = Quaternion.Euler(29.999f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                }
                else if (camera_pitch < -30)
                {
                    VerticalGimbal.transform.rotation = Quaternion.Euler(-29.999f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                }
            }
        }
    }
}