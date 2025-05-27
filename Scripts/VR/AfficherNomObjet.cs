using TMPro;
using UnityEngine;

public class AfficherNomObjet : MonoBehaviour
{

    [SerializeField] GameObject afficherNomObjet;
    public void PrendreObjet(string name)
    {
        afficherNomObjet.SetActive(true);
        afficherNomObjet.GetComponent<TMP_Text>().text = name;
    }

    public void LacherObjet()
    {
        afficherNomObjet.SetActive(false);
    }
}
