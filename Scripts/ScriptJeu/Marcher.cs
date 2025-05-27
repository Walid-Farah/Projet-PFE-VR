using UnityEngine;
using UnityEngine.EventSystems;

public class Marcher : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private float speed = 3.0F;
    private float jumpSpeed = 8.0F;
    private float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    float mouvementH, mouvementV;

   

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();


        if (SceneMoteur.entrer)
        {
            transform.position = SceneMoteur.positionJoueur;
        }
        
    }

   
    // Update is called once per frame
    void Update()
    {
        
        if (!PauseMenu.gamePaused)
        {

            //mouvementV += Input.GetAxis("Mouse Y");
            //mouvementV = Mathf.Clamp(mouvementV, -45f, 45f);


            mouvementH += Input.GetAxis("Mouse X") * 5f;
            transform.rotation = Quaternion.Euler(0f, mouvementH, 0f);


            if (controller.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;

                if (moveDirection != Vector3.zero)
                {
                    animator.SetBool("IsWalking", true);
                }
                else
                {
                    animator.SetBool("IsWalking", false);
                }

                if (Input.GetButton("Jump"))
                    moveDirection.y = jumpSpeed;

            }

            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }


    }
}
