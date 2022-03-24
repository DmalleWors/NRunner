using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herocontroller : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpHeight = 2f;

    private float gravity = -50f;
    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private float horizontalInput;
 
    // Start is called before the first frame update
    void Start()
    {
       characterController = GetComponent<CharacterController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = 1; 
            //faceforward
        transform.forward = new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);
            //is grounded
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore);

           
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0; 
        }
        else
        {
         velocity.y += gravity * Time.deltaTime; 
        }
        
        characterController.Move(new Vector3(horizontalInput * runSpeed, 0, 0) * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        characterController.Move(velocity * Time.deltaTime);
    }
}
