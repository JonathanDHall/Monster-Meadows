using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _instance;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform _camera;
    [SerializeField] private CinemachineFreeLook _cinMachine;

    private CharacterController controller;
    private float _speed = 6f;
    private float gravity = -19.62f;
    private float jumpHeight = 2;

    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    private float groundDIstance = 0.4f;
    public LayerMask groundMask;
    
    Vector3 velocity;
    bool isGrounded;
    bool _disableControls;

    void Start()
    {
        if (_instance == null)
            _instance = this;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (_disableControls)
            return;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDIstance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * _speed * Time.deltaTime);

            anim.SetBool("IsRunning", true);
        }
        else
            anim.SetBool("IsRunning", false);



        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            //anim.SetTrigger("Jump");
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void Death()
    {
        //Fill In Later
    }

    public void DisableControls()
    {
        _disableControls = true;
        _cinMachine.enabled = false;
        anim.SetBool("IsRunning", false);
    }

    public void EnableControls()
    {
        _disableControls = false;
        _cinMachine.enabled = true;
    }
}