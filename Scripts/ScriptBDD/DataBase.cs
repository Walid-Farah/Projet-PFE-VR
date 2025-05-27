using UnityEngine;
using Mono.Data.Sqlite;
using System.Collections;
using System;
using System.Data;
using System.Data.Common;
using UnityEngine.Windows;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEditor.SearchService;
using Unity.PlasticSCM.Editor.WebApi;


public class DataBase : MonoBehaviour
{
    private static string connectonString= "URI=file:Users.s3db";
    public TMP_InputField email;
    public TMP_InputField nom;
    public TMP_InputField prenom;
    public TMP_InputField mdp;
    public TMP_Text erreur;
    private Regex expressionPourMail;

    string query;

    public static string nomPersonne="Test",prenomPersonne="Test",emailPersonne="Test";
    public static int idPersonne=5,niveauPersonne = 0, NbrPoints = 0, NbrEssaie=0;



    void Start()
    {
        expressionPourMail = new Regex(@"\w+@[A-z]+\.[a-z]+");
        //Debug.Log(expressionPourMail.IsMatch("ds"));
        //connectonString = "URI=file:Users.s3db";

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Connexion()
    {
        if(email.text=="admin" && mdp.text == "admin")
        {
            SceneManager.LoadScene("Admin");
        }
        else
            using (IDbConnection connection = new SqliteConnection(connectonString))
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {

                    query = $"SELECT count(*) FROM Personne where email=\"{email.text}\"";
                    command.CommandText = query;

                    int rowCount = Convert.ToInt32(command.ExecuteScalar());

                    if (rowCount == 1)
                    {
                        query = $"SELECT * FROM Personne where email=\"{email.text}\"";
                        emailPersonne = email.text;
                        command.CommandText = query;
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            string motdepasse = reader.GetString(2);
                            if (mdp.text == motdepasse)
                            {
                                idPersonne = reader.GetInt16(0);
                                nomPersonne = reader.GetString(3);
                                prenomPersonne = reader.GetString(4);
                                niveauPersonne = reader.GetInt16(5);
                                ManageScene.LoadSampleScene();
                            }
                            else
                            {
                                erreur.SetText("Mot de passe faux");
                                Debug.Log("Mot de passe faux");
                            }
                            connection.Close();
                            reader.Close();
                        }
                    }
                    else
                    {
                        erreur.SetText("Compte Inexistant");
                        Debug.Log("Inexistant");
                    }

                }
            }
    }




    public void Inscription()
    {
        using (IDbConnection connection = new SqliteConnection(connectonString))
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                try
                {
                    

                    if ((email.text.Equals("")) || (mdp.text).Equals("") || (nom.text).Equals("") || (prenom.text).Equals(""))
                    {
                        erreur.SetText("Veuillez Remplir Tous Les Champs");
                    }
                    else
                    {
                        if (!expressionPourMail.IsMatch(email.text))
                        {
                            erreur.SetText("Email Non Valide");
                        }
                        else {
                            string sql = $"INSERT into Personne(email,mdp,nom,prenom) VALUES (\"{email.text}\",\"{mdp.text}\",\"{nom.text}\",\"{prenom.text}\")";
                            emailPersonne = email.text;
                            nomPersonne = nom.text;
                            prenomPersonne = prenom.text;
                            command.CommandText = sql;
                            command.ExecuteNonQuery();

                            //recuperer id

                            command.CommandText = $"SELECT id FROM Personne WHERE email=\"{email.text}\"";
                            idPersonne = (int)command.ExecuteScalar();

                            connection.Close();
                            ManageScene.LoadSampleScene();
                        }
                    }
                    

                }
                catch (Exception)
                {
                    erreur.SetText("Compte Existant Reesayer");
                    Debug.Log("Compte Existant Reesayer");
                }
            }
        }
    }

    public static void CreationLignePE(int idScene)
    {
        using(IDbConnection connection=new SqliteConnection(connectonString))
        {
            connection.Open();
            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = $"INSERT OR IGNORE INTO PointEssaie(idPersonne,idPiece) VALUES ({idPersonne},{idScene})";
                command.ExecuteNonQuery();
            }
        }
    }
    public static void PointEssaie(int idScene,int nbrPoint)
    {
        using (IDbConnection connection = new SqliteConnection(connectonString))
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = $"UPDATE PointEssaie set NbrPoints={nbrPoint},NbrEssaie=NbrEssaie+1 WHERE idPersonne={idPersonne} and idPiece={idScene}";
                command.ExecuteNonQuery();
            }
        }
    }



    

}
