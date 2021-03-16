using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public static List<Card> staticDeck = new List<Card>();
    public Card container = new Card();
    public int x;
    public static int deckSize;

    private int randomIndex;

    public GameObject cardInDeck1;
    public GameObject cardInDeck2;
    public GameObject cardInDeck3;
    public GameObject cardInDeck4;

    public GameObject CardToHand;

    public Deck()
	{

	}
    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        deckSize = 30;

        for(int i=0; i<deckSize; i++)
		{
            x = Random.Range(1, 7);
            deck[i] = CardDataBase.cardList[x];
		}
        staticDeck = deck;
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {

        if(deckSize<30)
		{
            cardInDeck1.SetActive(false);
		}
        if (deckSize < 20)
        {
            cardInDeck2.SetActive(false);
        }
        if (deckSize < 10)
        {
            cardInDeck3.SetActive(false);
        }
        if (deckSize == 0)
        {
            cardInDeck4.SetActive(false);
        }
        if (deckSize > 0)
        {
            cardInDeck4.SetActive(true);
        }
        if (deckSize > 9)
        {
            cardInDeck3.SetActive(true);
        }
        if (deckSize > 19)
        {
            cardInDeck2.SetActive(true);
        }
        if (deckSize > 29)
        {
            cardInDeck1.SetActive(true);
        }

        if(SystemeDeTour.startTurn == true)
		{
            Pioche();
            SystemeDeTour.startTurn = false;
        }
    }

    public void Shuffle()
    {
        for (int i = 0; i < deckSize; i++)
        {
            container = deck[i];
            randomIndex = Random.Range(i, deckSize);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container;
        }
    }

    public void Pioche()
	{
        StartCoroutine(PiocheOne());
    }

    IEnumerator StartGame()
	{
        for(int i=0; i<=4; i++)
		{
            yield return new WaitForSeconds(0.5f);
            Instantiate(CardToHand, transform.position, transform.rotation);
		}
	}

    IEnumerator PiocheOne()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(CardToHand, transform.position, transform.rotation);
    }
}
