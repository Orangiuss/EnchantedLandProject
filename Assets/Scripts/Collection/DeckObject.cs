using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckObject : MonoBehaviour
{
    public TextMeshProUGUI numberOfCards;

    public Image image;
    public GameObject heros;
    public int deckId;
    public int heroId;
    // Start is called before the first frame update
    void Start()
    {
        this.numberOfCards.text = "Vide";
        this.heros.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(int n, int hero, int deckId)
	{
        if (n != 0)
        {
            if (n != 30) { this.numberOfCards.color = Color.red; }
            this.numberOfCards.text = n + " Cartes";
            this.heros.SetActive(true);
            this.image.sprite = Resources.Load<Sprite>("Heros" + hero);
            this.deckId = deckId;
            this.heroId = hero;
        }
        return;
    }
}
