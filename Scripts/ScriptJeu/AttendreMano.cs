using System;
using System.Collections;
using UnityEngine;

public class AttendreMano : MonoBehaviour
{
    //Vector3 positionInitJauge;
    private void Start()
    {
        //positionInitJauge = transform.GetChild(1).localEulerAngles;
        //Debug.Log(positionInitJauge);
    }

    void Update()
    {

    }

    public IEnumerator Attendre()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
    
}
