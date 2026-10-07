using UnityEngine;

public class AnimTester : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private float turnSpeed = 10f;

    [SerializeField, Tooltip("Degrees added to the facing so the model's chest leads. Set to 0 once the model faces +Z.")]
    private float modelYawOffset = 180f;

    private Animator anim;
    private Camera cam;

    void Awake()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    void Update()
    {
        // WASD / arrows = move (camera-relative), 3 = Attack, 4 = Death
        Vector3 forward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cam.transform.right, Vector3.up).normalized;

        Vector3 input = right * Input.GetAxisRaw("Horizontal") + forward * Input.GetAxisRaw("Vertical");
        input = Vector3.ClampMagnitude(input, 1f);

        if (input != Vector3.zero)
        {
            transform.position += input * (moveSpeed * Time.deltaTime);

            Quaternion targetRot = Quaternion.LookRotation(input) * Quaternion.Euler(0f, modelYawOffset, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
        }

        anim.SetFloat("Speed", input.magnitude * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            anim.SetTrigger("Death");
        }
    }
}
