using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public float moveSpeed;
    private Rigidbody myRigidbody;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera mainCamera;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength; 

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            //Debug.DrawLine(cameraRay.origin, pointToLook, Color.white);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = moveVelocity;
    }
}