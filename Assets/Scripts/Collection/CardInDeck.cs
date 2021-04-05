using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInDeck : MonoBehaviour
{
    public Card thisCard = new Card();
    public int thisId;
    public string CardName;

    public TextMeshProUGUI nameText;

    public Sprite sprite;
    public Image image;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
	{
        this.gameObject.SetActive(true);
        thisCard = CardDataBase.cardList[thisId];

        CardName = thisCard.CardName;
        sprite = thisCard.Image;
        image.sprite = sprite;

        nameText.text = "" + CardName;
    }

    public void Remove()
	{
        this.gameObject.SetActive(false);
    }

}
