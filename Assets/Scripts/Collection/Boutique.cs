using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using TMPro;

public class Boutique : MonoBehaviour
{
    private string databaseURL = "https://enchantedland-ae7db-default-rtdb.firebaseio.com/collections/";

    public Collection collection;

    public List<DeckCollection> decks = new List<DeckCollection>();

    public int Carte1, Carte2, Carte3;

    public ThisCardCollection Card1, Card2, Card3;

    public GameObject plusButton;
    public GameObject moinsButton;
    public GameObject paquetAOuvrir;

    public TextMeshProUGUI nombrePaquets;
    public TextMeshProUGUI coutPaquets;
    public TextMeshProUGUI nombrePaquetsAOuvrirText;
    public TextMeshProUGUI orText;

    public int nombrePaquetsInt;
    public int nombrePaquetsAOuvrir;

    public int orDisponible;

    private void Start()
	{
        CollectionFromDatabase();
        orDisponible = 500;
        orText.text = "" + orDisponible;
        nombrePaquetsInt = 1;
        nombrePaquetsAOuvrir = 0;
        nombrePaquets.text = "" + 1;
        coutPaquets.text = "" + 100;
        nombrePaquetsAOuvrirText.text = "×" + 0;
        moinsButton.SetActive(false);
        plusButton.SetActive(true);
        paquetAOuvrir.SetActive(false);
    }

	private void Update()
	{
        if (nombrePaquetsAOuvrir == 0)
        {
            paquetAOuvrir.SetActive(false);
        }
    }

	private void CollectionFromDatabase()
    {
        string localId = PlayerPrefs.GetString("localIdPlayer");
        RestClient.Get<Collection>(url: databaseURL + localId + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer")).Then(onResolved: response =>
        {
            collection = response;
            decks = response.decks; 
        }).Catch(error =>
        {
            Debug.Log(error);
        });
    }

    public void OuvrirPaquet()
    {
        nombrePaquetsAOuvrir--;
        nombrePaquetsAOuvrirText.text = "×" + nombrePaquetsAOuvrir;
        string localId = PlayerPrefs.GetString("localIdPlayer");
        Carte1 = Random.Range(1, 30);
        Debug.Log(Carte1);
        Carte2 = Random.Range(1, 30);
        Debug.Log(Carte2);
        Carte3 = Random.Range(1, 30);
        Debug.Log(Carte3);
        collection.collection.Add(Carte1);
        collection.collection.Add(Carte2);
        collection.collection.Add(Carte3);
        collection.collection.Sort();
        collection.decks = decks;
        RestClient.Put(url: databaseURL + localId + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"), collection);
        Card1.thisId = Carte1;
        Card2.thisId = Carte2;
        Card3.thisId = Carte3;
        Card1.Initialize();
        Card2.Initialize();
        Card3.Initialize();
    }

    public void Plus()
	{
        nombrePaquetsInt++;
        nombrePaquets.text = "" + nombrePaquetsInt;
        moinsButton.SetActive(true);
        coutPaquets.text = "" + nombrePaquetsInt*100;
    }

    public void Moins()
	{
        if(nombrePaquetsInt - 1 >= 1)
		{
            nombrePaquetsInt--;
            nombrePaquets.text = "" + nombrePaquetsInt;
        }
        if(nombrePaquetsInt == 1)
		{
            moinsButton.SetActive(false);
        }
        coutPaquets.text = "" + nombrePaquetsInt * 100;
    }

    public void Acheter()
	{
        if (orDisponible - (nombrePaquetsInt * 100) >= 0)
        {
            nombrePaquetsAOuvrir = nombrePaquetsInt;
            nombrePaquetsAOuvrirText.text = "×" + nombrePaquetsAOuvrir;
            paquetAOuvrir.SetActive(true);
            orDisponible = orDisponible - (nombrePaquetsInt * 100);
            orText.text = "" + orDisponible;
        }
    }
}
