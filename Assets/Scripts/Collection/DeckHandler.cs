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

    public static DeckCollection newDeck = new DeckCollection();
    public DeckCollection newDec = new DeckCollection();
    public Collection collection;
    public GameObject panelNewDeck;
    public static bool newDeckCreate;

    public CardInDeck[] cardindeck1;
    public CardInDeck[] cardindeck2;

    public int nbrDecks;

    public bool choixDeck;
    // Start is called before the first frame update
    void Start()
    {
        newDeckCreate = false;
        panelNewDeck = GameObject.Find("panelNewDeck");
        if (!choixDeck)
        {
            cardindeck1 = GameObject.Find("CardsInDeckPanel1").GetComponentsInChildren<CardInDeck>();
            cardindeck2 = GameObject.Find("CardsInDeckPanel2").GetComponentsInChildren<CardInDeck>();
        }
        CardsInDeckClear();
        if (panelNewDeck != null)
        {
            panelNewDeck.SetActive(false);
        }
        decksObjects = GameObject.Find("Decks");
        decksList = decksObjects.GetComponentsInChildren<DeckObject>();
        decks.Clear();
        DecksFromDataBase();
    }

    // Update is called once per frame
    void Update()
    {
        newDec = newDeck;
    }

    public void CreateNewDeck()
	{
        newDeckCreate = true;
        panelNewDeck.SetActive(true);
    }

    public void CreateNewDeckStop()
    {
        newDeckCreate = false;
        newDeck.deck.Clear();
        CardsInDeckClear();
        panelNewDeck.SetActive(false);
    }

    public void CreateNewDeckValidate()
    {
        newDeckCreate = false;
        CardsInDeckClear();
        panelNewDeck.SetActive(false);
        PutNewDeckDataBase();
    }

    public void UpdateNewDeck(int id)
	{
        bool ok = false;
        foreach (CardInDeck card in cardindeck1)
        {
            if(!card.gameObject.activeSelf)
			{
                card.thisId = id;
                card.Initialize();
                ok = true;
                break;
            }
        }
        if(!ok)
		{
            foreach (CardInDeck card in cardindeck2)
            {
                if (!card.gameObject.activeSelf)
                {
                    card.thisId = id;
                    card.Initialize();
                    ok = true;
                    break;
                }
            }
        }
    }

    public void UpdateNewDeckRemove(int id)
	{
        bool ok = false;
        foreach (CardInDeck card in cardindeck1)
        {
            if (card.gameObject.activeSelf && card.thisId == id)
            {
                card.Remove();
                ok = true;
                break;
            }
        }
        if (!ok)
        {
            foreach (CardInDeck card in cardindeck2)
            {
                if (card.gameObject.activeSelf && card.thisId == id)
                {
                    card.Remove();
                    ok = true;
                    break;
                }
            }
        }
    }

    public void CardsInDeckClear()
	{
        foreach (CardInDeck card in cardindeck1)
        {
            card.Remove();
        }
        foreach (CardInDeck card in cardindeck2)
        {
            card.Remove();
        }
    }

    private void PutNewDeckDataBase() {
        nbrDecks = decks.Count;
        for (int i = 0; i < nbrDecks; i++)
        {
            if (decks[i].deck.Count == 0)
			{
                decks[i].deck = newDeck.deck;
                collection.decks = decks;
                RestClient.Put<Collection>(url: databaseURL + PlayerPrefs.GetString("localIdPlayer") + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"), collection).Then(onResolved: response4 =>
                {
                    DecksFromDataBase();
                    newDeck.deck.Clear();
                });
                break;
            }
        }
    }

    private void DecksFromDataBase()
    {
        string localId = PlayerPrefs.GetString("localIdPlayer");
        RestClient.Get<Collection>(url: databaseURL + localId + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer")).Then(onResolved: response =>
        {
            collection = response;
            decks = collection.decks;
            nbrDecks = decks.Count;
            for (int i = 0; i < nbrDecks; i++)
            {
                decksList[i].Initialize(decks[i].deck.Count, decks[i].heros, i+1);
            }
        }).Catch(error =>
        {
            Debug.Log(error);
        });
    }
}
