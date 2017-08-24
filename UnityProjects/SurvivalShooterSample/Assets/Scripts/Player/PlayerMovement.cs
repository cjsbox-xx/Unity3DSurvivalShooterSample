using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 6.0f;

    private const string WALKING_PARAM = "IsWalking";
    private Vector3 movement;
    private Animator animator;
    private Rigidbody playerRigidBody;
    private int floorMask;
    private float camRayLength = 100.0f;

    public Rigidbody GetRigidbody()
    {
        return playerRigidBody;
    }

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0.0f, v);
        movement = movement.normalized * Speed * Time.deltaTime;
        playerRigidBody.MovePosition(transform.position + movement);
    }


    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0.0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0.0f || v != 0.0f;
        animator.SetBool(WALKING_PARAM, walking);
    }

    void PlaySound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, transform.position);
    }

}
