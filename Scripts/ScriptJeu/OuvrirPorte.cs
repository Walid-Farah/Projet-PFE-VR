using UnityEngine;
using UnityEngine.InputSystem;

public class OuvrirPorte : MonoBehaviour
{
    public GameObject Instruction;
    public GameObject porte1;
    public GameObject porte2;


    public InputVR inputActions;


    private bool Action = false;
    private bool Ouvert = true;


    private void Awake()
    {
        inputActions = new InputVR();

    }
    void Start()
    {
        Instruction.SetActive(false);

    }


    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.transform.name);
        if (collision.transform.tag == "Player")
        {
            Instruction.SetActive(true);
            Action = true;
            inputActions.Player.OuvrirPorte.performed += OuvrirFermerPorte;
        }  
    }

    void OnTriggerExit(Collider collision)
    {
        Instruction.SetActive(false);
        Action = false;
        inputActions.Player.OuvrirPorte.performed -= OuvrirFermerPorte;
    }


    public void OuvrirFermerPorte(InputAction.CallbackContext callbackContext)
    {
        if (Ouvert)
        {

            Instruction.SetActive(false);
            porte1.GetComponent<Animator>().Play("OuvrirPorte");
            if (porte2 != null)
            {
                porte2.GetComponent<Animator>().Play("OuvrirPorte2");
            }
            Ouvert = false;
        }

        else
        {
            Instruction.SetActive(false);
            Ouvert = true;
            porte1.GetComponent<Animator>().Play("FermerPorte");
            if (porte2 != null)
            {
                porte2.GetComponent<Animator>().Play("FermerPorte2");
            }
        }

    }

   

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}


//void Update()
//{


//    if (Input.GetKeyDown(KeyCode.E))
//    {
//        if (Action == true)
//        {
//            if (Ouvert)
//            {

//                Instruction.SetActive(false);
//                porte1.GetComponent<Animator>().Play("OuvrirPorte");
//                if (porte2 != null)
//                {
//                    porte2.GetComponent<Animator>().Play("OuvrirPorte2");
//                }
//                Ouvert = false;
//            }
//            else
//            {
//                Instruction.SetActive(false);
//                Ouvert = true;
//                porte1.GetComponent<Animator>().Play("FermerPorte");
//                if (porte2 != null)
//                {
//                    porte2.GetComponent<Animator>().Play("FermerPorte2");
//                }
//            }

//        }
//    }

//}