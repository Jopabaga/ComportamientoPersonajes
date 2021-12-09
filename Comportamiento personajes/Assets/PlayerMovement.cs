using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float distanciaSuelo;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravedad;

    private Vector3 moveDriection;
    private Vector3 velocity;

    private CharacterController controller;
    private Animator anim;

    public PlayerStats stats;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }
    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals("bala") == true)
        {

            stats.hp = stats.hp - 1;
            Debug.Log(stats.hp);
        }
    }

    private void Update()
    {
        Move();
        if (stats.isDead)
        {
            anim.SetFloat("speed", 1.0f);
            SceneManager.LoadScene(0);
        }
    }
    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, distanciaSuelo, groundMask);
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");
        moveDriection = new Vector3(moveX, 0, moveZ);
        moveDriection = transform.TransformDirection(moveDriection);
        
        if(moveDriection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();

        }
        else if(moveDriection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }else if (moveDriection == Vector3.zero)
        {
            Idle();
        }
        moveDriection *= moveSpeed;
        controller.Move(moveDriection * Time.deltaTime);
    }
    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("speed", 0.25f);

    }
    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("speed", 0.75f);
    }
    private void Idle()
    {
        anim.SetFloat("speed", 0f);
    }
}
