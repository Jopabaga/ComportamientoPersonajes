using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializedField] private float moveSpeed;
    [SerializedField] private float walkSpeed;
    [SerializedField] private float runSpeed;

    private Vector3 moveDriection;
    private CharachterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {

    }
}
