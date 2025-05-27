using System.Data;
using Mono.Data.Sqlite;
using TMPro;
using UnityEngine;

public class AfficherScorePer : MonoBehaviour
{

    Transform entryContainer;
    Transform entryTemplate;

    private void Awake()
    {
        AfficherPersonnesNbrPoint();
    }


    public void AfficherPersonnesNbrPoint()
    {
        entryContainer = transform.Find("ContainerScore");
        entryTemplate = entryContainer.Find("ScorePersonne");

        float templateHeight = -100f;
        int i = 1;

        using (IDbConnection connection = new SqliteConnection("URI = file:Users.s3db"))
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Personne.nom,Personne.prenom,SUM(NbrPoints) as NbrPoints,SUM(NbrEssaie) AS NbrEssaie FROM Personne,Piece,PointEssaie WHERE PointEssaie.idPersonne=Personne.id and PointEssaie.idPiece=Piece.id GROUP BY Personne.id ORDER BY NbrPoints DESC, NbrEssaie ASC";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
                        entryTransform.Find("NomP").GetComponent<TextMeshProUGUI>().SetText($"{reader.GetString(0)}");
                        entryTransform.Find("PrenomP").GetComponent<TextMeshProUGUI>().SetText($"{reader.GetString(1)}");
                        entryTransform.Find("ScoreP").GetComponent<TextMeshProUGUI>().SetText($"{reader.GetInt16(2)}");
                        entryTransform.Find("NbrEssaieP").GetComponent<TextMeshProUGUI>().SetText($"{reader.GetInt16(3)}");
                        entryTransform.localPosition = new Vector3(0, templateHeight * i, 0);
                        i++;
                    }
                }
            }
        }

        Destroy(entryTemplate.gameObject);
    }


}
