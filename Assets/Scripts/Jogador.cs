using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Jogador : MonoBehaviour
{
  
    CharacterController controller;
    Vector2 moveInput;
    [SerializeField] float valorGravidade;
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;

    Vector3 verticalVelocity;
    bool isGrounded;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

   
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        isGrounded = controller.isGrounded;

        if(isGrounded && verticalVelocity.y <= 0)
        {
            verticalVelocity.y = -2f;
        }

        Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;
        verticalVelocity.y = valorGravidade * Time.deltaTime;
        Vector3 finalVelocity = moveDirection*speed;

        verticalVelocity.y += valorGravidade * Time.deltaTime;
        finalVelocity.y = verticalVelocity.y;
        controller.Move(finalVelocity * Time.deltaTime);
    }

    public void OnMove(InputValue valor)
    {
        moveInput = valor.Get<Vector2>();
    }

    public void OnJump(InputValue valor)
    {
        if(isGrounded)
        {
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * valorGravidade);
        }
    } 
}
