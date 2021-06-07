using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;

    private CharacterController playerController;

    Camera viewCamera;

    private Vector3 movementThreshold = new Vector3();

    public float stopPos;

    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + new Vector3(-0.1f, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + new Vector3(0.1f, 0, 0);
        }*/

        float moveX = Input.GetAxis("Horizontal");
        float moxeZ = Input.GetAxis("Vertical");

        movementThreshold = new Vector3(moveX, 0.0f, moxeZ);
        playerController.Move(movementThreshold * Time.deltaTime * moveSpeed);

        movementThreshold = movementThreshold.normalized;

        if (movementThreshold.magnitude < stopPos)
        {
            movementThreshold = Vector3.zero;
        }
    }
}
