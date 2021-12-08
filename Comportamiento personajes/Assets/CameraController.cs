using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSens;

    private Transform parent;

    private void Start()
    {
        parent = transform.parent;
    }
    private void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        parent.Rotate(Vector3.up, mouseX);
    }
}
