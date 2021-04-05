using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Proyecto26;
using TMPro;

public class PlayerMenu : MonoBehaviour
{
    private string databaseURL = "https://enchantedland-ae7db-default-rtdb.firebaseio.com/";
    private string AuthKey = "AIzaSyD_kvrwhmg8uQCSNFmZYhzR4lHpDoTZkrA";

    public bool activePanel;
    public GameObject infosComptePanel;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI emailText;

    void Start()
    {
        if (PlayerPrefs.GetString("localIdPlayer") != null)
        {
            Debug.Log("Vous etes bien connecté !");
        }
        else
		{
            Debug.Log("GROSSE ERREUR !");
		}

        activePanel = false;
        infosComptePanel = GameObject.Find("InfosCompte");
        infosComptePanel.SetActive(activePanel);
        GetUserInformations();
    }

    // Update is called once per frame
    void Update()
    {
        infosComptePanel.SetActive(activePanel);
        //GetUserInformations(user);
    }

    private void GetUserInformations()
	{
        RestClient.Get<User>(url: databaseURL + "users/" + PlayerPrefs.GetString("localIdPlayer") + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer")).Then(onResolved: response =>
        {
            nameText.text = "Nom :" + response.nomUtilisateur;
            emailText.text = "Email :" + response.email;
        });
    }

    public void AfficherInfos()
	{
        if (activePanel == false)
		{
            activePanel = true;
        }
        else
		{
            activePanel = false;
        }
	}

    public void SupprimerCompte()
	{
        DeleteAccount();
    }

    private void DeleteAccount()
	{
        string userData = "{\"idToken\":\"" + PlayerPrefs.GetString("IdTokenPlayer") + "\"}";
        RestClient.Post("https://identitytoolkit.googleapis.com/v1/accounts:delete?key=" + AuthKey, userData).Then(
        response =>
        {
            RestClient.Delete(url: databaseURL + "users/" + PlayerPrefs.GetString("localIdPlayer") + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer")).Then(onResolved: response2 =>
            {
                RestClient.Delete(url: databaseURL + "collections/" + PlayerPrefs.GetString("localIdPlayer") + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer")).Then(onResolved: response3 =>
                {
                    SceneManager.LoadScene("RegisterLogin");
                    Debug.Log("Compte supprimé");
                }).Catch(error =>
                {
                    Debug.Log(error);
                });
            }).Catch(error =>
            {
                Debug.Log(error);
            });
        }).Catch(error =>
        {
            Debug.Log(error);
        });
    }
}
