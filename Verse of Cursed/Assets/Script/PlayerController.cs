using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 500f;

    [Header("Ground Check Setting")]
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private LayerMask groundLayer;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 1000f;
    [SerializeField] private bool allowJump = true;

    private bool isGrounded;

    private Quaternion targetRotation;

    private CameraFollow cameraController;
    private CharacterController characterController;
    private float ySpeed;
    
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject player01;
    [SerializeField] GameObject player02;
    


    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraFollow>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

        var moveInput = (new Vector3(horizontal, 0, vertical)).normalized;

        var moveDir = cameraController.PlanarRotation * moveInput;

        GroundCheck();
        SkillCheck();

        if (isGrounded)
        {
            ySpeed = -0.5f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }

        var velocity = moveDir * moveSpeed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (moveAmount > 0)
        {
            characterController.Move(moveDir * moveSpeed * Time.deltaTime);
            targetRotation = transform.rotation = Quaternion.LookRotation(moveDir);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
    void Jump()
    {
        float jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpForce);
        characterController.Move(Vector3.up * jumpVelocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }
    
    private void SkillCheck()
    {
        Mesh playerMesh = GetMesh(playerObject);
        Mesh p1Mesh = GetMesh(player01);
        Mesh p2Mesh = GetMesh(player02);

        if (AreMeshesEqual(playerMesh, p1Mesh))
        {
            Debug.Log("Player mesh is the same as p1 mesh.");
            moveSpeed = 6f;
            jumpForce = 2000f;
        }

        if (AreMeshesEqual(playerMesh, p2Mesh))
        {
            Debug.Log("Player mesh is the same as p2 mesh.");
            jumpForce = 30000f;
            moveSpeed = 2f;
        }
    }

    Mesh GetMesh(GameObject obj)
    {
        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
    
        if (meshFilter != null)
        {
            return meshFilter.sharedMesh;
        }
        else
        {
            Debug.LogError("MeshFilter not found on the GameObject.");
            return null;
        }
    }

    bool AreMeshesEqual(Mesh mesh1, Mesh mesh2)
    {
   
        return mesh1.vertices.Length == mesh2.vertices.Length;
    }
}
