using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccederGroupeVR : MonoBehaviour
{

    [SerializeField] GameObject accederMoteur;
    InputVR inputAction;

    void Start()
    {
        inputAction = new InputVR();
        inputAction.Enable();
    }

    
    

    private void OnTriggerEnter(Collider other)
    {
        accederMoteur.SetActive(true);
        inputAction.Player.OuvrirPorte.performed += OuvrirPorte_performed;
        Debug.Log("Entrer");
    }

    

    private void OnTriggerExit(Collider other)
    {
        inputAction.Player.OuvrirPorte.performed-= OuvrirPorte_performed;
        accederMoteur.SetActive(false);
        Debug.Log("Sortie");
    }


    private void OuvrirPorte_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene("Moteur");
        Debug.Log("Appuyer");
    }




    //private void OnEnable()
    //{
    //    inputAction.Enable();
    //}
    private void OnDisable()
    {
        inputAction.Disable();
    }
}
