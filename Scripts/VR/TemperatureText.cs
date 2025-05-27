using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.VisualScripting;

public class TemperatureText : MonoBehaviour
{

    [SerializeField] Transform ligneTermometre;
    [SerializeField] TMP_Text temperature;
    [SerializeField] GameObject laser;
    Renderer mateLaser;

    InputVR inputActions;
    XRGrabInteractable interactable;
    IXRSelectInteractor interactor;

    bool voirChoixManette;
    string temperatureText;


    private void Awake()
    {
        inputActions = new InputVR();
        inputActions.Temperature.VoirTemp.performed += VoirTemp_performed;
        
        mateLaser=laser.GetComponent<Renderer>();
        mateLaser.material.color = Color.black;


        interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(OnGrab);
        interactable.selectExited.AddListener(OnRelease);
    }

    private void OnRelease(SelectExitEventArgs arg0)
    {
        laser.SetActive(false);
        interactor = null;
    }

    private void OnGrab(SelectEnterEventArgs arg0)
    {
        laser.SetActive(true);
        interactor = arg0.interactorObject;


        if (interactor != null)
        {
            voirChoixManette = (interactor.transform.CompareTag("LeftController")) ? true : false;
        }

    }


    private void VoirTemp_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (voirChoixManette)
        {
            temperature.SetText(temperatureText);
        }
        else
        {
            temperature.SetText(temperatureText);
        }

    }

    private void Update()
    {

        if (Physics.Raycast(ligneTermometre.position, ligneTermometre.TransformDirection(Vector3.forward), out RaycastHit rayInfo, 1.5f))
        {
            Debug.DrawRay(ligneTermometre.position, transform.TransformDirection(Vector3.forward) * rayInfo.distance, Color.red);
            if (rayInfo.transform.CompareTag("Temperature"))
            {
                temperatureText = rayInfo.collider.name;
                Debug.Log("Toucher!!!!!!!!!!!!!!!!!!!!!!     " + rayInfo.collider.name+" distance: "+rayInfo.distance);
                mateLaser.material.color=Color.green;
            }
            else
            {
                temperatureText = "Lo";
                Debug.Log("hhe");
                mateLaser.material.color = Color.red;
            }
        }
        else
        {
            mateLaser.material.color = Color.red;
            Debug.DrawRay(ligneTermometre.position, transform.TransformDirection(Vector3.forward) * rayInfo.distance, Color.red);
            temperatureText = "Lo";
        }
    }


    private void OnEnable()
    {
        inputActions.Enable();
    }


    private void OnDisable()
    {
        inputActions.Disable();
        interactable.selectEntered.RemoveAllListeners();
    }

}
