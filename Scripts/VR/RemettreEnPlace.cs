using UnityEngine;
using UnityEngine.SceneManagement;

public class RemettreEnPlace : MonoBehaviour
{

    InputVR inputActions;

    [SerializeField] GameObject vanneAspiration;
    [SerializeField] GameObject vanneBRL;
    [SerializeField] GameObject vanneRefoulement;


    Vector3 vanneAsPos, vanneBRPos, vanneRePos;
    Quaternion vanneAsRot, vanneBRRot, vanneReRot;

    private void Awake()
    {
        inputActions=new InputVR();
        inputActions.Player.RemettreEnPlace.performed += _ => RemetEnPlace();

        vanneAsPos = vanneAspiration.transform.position;
        vanneBRPos = vanneBRL.transform.position;
        vanneRePos = vanneRefoulement.transform.position;

        vanneReRot = vanneRefoulement.transform.rotation;
        vanneBRRot = vanneBRL.transform.rotation;
        vanneAsRot = vanneAspiration.transform.rotation;
    }


    void Update()
    {
        
    }

    public void RemetEnPlace()
    {
        vanneAspiration.transform.position = vanneAsPos;
        vanneBRL.transform.position = vanneBRPos;
        vanneRefoulement.transform.position = vanneRePos;

        vanneRefoulement.transform.rotation = vanneReRot;
        vanneAspiration.transform.rotation = vanneAsRot;
        vanneBRL.transform.rotation = vanneBRRot;
    }


    public void PasserSceneUsine()
    {
        SceneManager.LoadScene("SampleScenes");
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


