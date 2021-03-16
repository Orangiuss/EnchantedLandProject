using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckEnnemy : MonoBehaviour
{
    public static int deckSize;

    public GameObject cardInDeck1;
    public GameObject cardInDeck2;
    public GameObject cardInDeck3;
    public GameObject cardInDeck4;

    public DeckEnnemy()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (deckSize < 30)
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
    }
}
