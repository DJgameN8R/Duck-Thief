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

    private GameManager gameManager;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        Enemy.OnGuardHasSpottedPlayer += Disable;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager.gameState == GameState.preGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameManager.gameState = GameState.game;
            }
        }

            if (gameManager.gameState == GameState.game)
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
            if (disabled)
            {
                moveInput = Vector3.zero;
                moveVelocity = Vector3.zero;
            }
        }
    }

    void OnTriggerEnter(Collider hitCollider)
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

    void FixedUpdate()
    {
        myRigidbody.velocity = moveVelocity;
    }

    void OnDestroy()
    {
        Enemy.OnGuardHasSpottedPlayer -= Disable;
    }
}