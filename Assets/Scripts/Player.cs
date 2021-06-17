using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public event System.Action OnReachedEndOfLevel;

    public float moveSpeed;
    private Rigidbody myRigidbody;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera mainCamera;

    bool disabled;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        Enemy.OnGuardHasSpottedPlayer += Disable;
    }

    void Update()
    {
        if (!disabled)
        {
            Vector3 moveinput = Vector3.zero;

            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput * moveSpeed;

            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                //Debug.DrawLine(cameraRay.origin, pointToLook, Color.white);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }
        if(disabled)
        {
            moveInput = Vector3.zero;
            moveVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider hitCollider)
    {
        if (hitCollider.tag == "Finish")
        {
            Disable();
            if (OnReachedEndOfLevel != null)
            {
                OnReachedEndOfLevel();
            }
        }
    }

    void Disable()
    {
        disabled = true;
    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = moveVelocity;
    }

    private void OnDestroy()
    {
        Enemy.OnGuardHasSpottedPlayer -= Disable;
    }
}