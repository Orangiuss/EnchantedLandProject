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
    public static bool startGame;

    private int randomIndex;
    private int fatigue;

    public GameObject cardInDeck1;
    public GameObject cardInDeck2;
    public GameObject cardInDeck3;
    public GameObject cardInDeck4;

    public GameObject CardToHand;

    public GameHandler gameHandler;

    public Deck()
	{

	}
    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        startGame = false;
        x = 0;
        fatigue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {
            StartCoroutine(StartGame());
            startGame = false;
        }
        if (deckSize<30)
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
            Pioche(1);
            SystemeDeTour.startTurn = false;
        }
    }

    public void Shuffle()
    {
        deck = staticDeck;
        for (int i = 0; i < deckSize; i++)
        {
            container = deck[i];
            randomIndex = Random.Range(i, deckSize);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container;
        }
        staticDeck = deck;
    }

    public void Pioche(int x)
	{
        StartCoroutine(PiocheOne(x));
    }

    IEnumerator StartGame()
	{
        int n;
        if(SystemeDeTour.isYourTurn) { n = 3; } 
        else
		{
            n = 4;
		}
        Shuffle();
        for (int i=0; i<n; i++)
		{
            yield return new WaitForSeconds(0.5f);
            Instantiate(CardToHand, transform.position, transform.rotation);
		}
	}

    IEnumerator PiocheOne(int x)
    {
        for (int i = 0; i < x; i++)
        {
            yield return new WaitForSeconds(0.5f);
            if (staticDeck.Count == 0)
            {
                fatigue++;
                PlayerHp.staticHp = PlayerHp.staticHp - fatigue;
                gameHandler.UpdatePlateaus();
            }
            else
            {
                Instantiate(CardToHand, transform.position, transform.rotation);
            }
        }
    }
}
