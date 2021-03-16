using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandler : MonoBehaviour
{
    private string databaseURL = "https://enchantedland-ae7db-default-rtdb.firebaseio.com/collections/";

    public GameObject decksObjects;
    public DeckObject[] decksList;
    public List<DeckCollection> decks = new List<DeckCollection>();

    public int nbrDecks; 
    // Start is called before the first frame update
    void Start()
    {
        decksObjects = GameObject.Find("Decks");
        decksList = decksObjects.GetComponentsInChildren<DeckObject>();
        decks.Clear();
        DecksFromDataBase();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DecksFromDataBase()
    {
        string localId = PlayerPrefs.GetString("localIdPlayer");
        RestClient.Get<Collection>(url: databaseURL + localId + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer")).Then(onResolved: response =>
        {
            decks = response.decks;
            nbrDecks = decks.Count;
            Debug.Log(nbrDecks);
            for (int i = 0; i < nbrDecks; i++)
            {
                decksList[i].Initialize(decks[i].deck.Count, decks[i].heros);
            }
        }).Catch(error =>
        {
            Debug.Log(error);
        });
    }
}
