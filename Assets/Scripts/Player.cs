using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody rb;
    [SerializeField] private float camOffset = -7;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private GameObject cam;

    void Start(){
        camOffset = cam.transform.position.x;
        rb = GetComponent<Rigidbody>();
    }

    private void OnMovement(InputValue val){
        movement = val.Get<Vector2>();
    }

    void FixedUpdate(){
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, movement.x, 0) * rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        Vector3 moveDirection = new Vector3(movement.y, 0, movement.y);
        moveDirection = rb.rotation * moveDirection;
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        cam.transform.position = new Vector3(rb.position.x + camOffset, cam.transform.position.y, rb.position.z + camOffset);
    }
}
