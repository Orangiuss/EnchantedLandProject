using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeckPanel : MonoBehaviour
{
    public Text logoText;
    // Start is called before the first frame update
    void Start()
    {
        logoText.text = "Cartes restantes:" + Deck.deckSize;
        logoText.enabled = true;
    }

    void Update()
    {
        logoText.text = "Cartes restantes:" + Deck.deckSize;
    }
}
