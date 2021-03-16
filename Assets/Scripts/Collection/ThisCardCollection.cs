using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ThisCardCollection : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public Card thisCard = new Card();
    [Range(0, 4)]
    public int thisId;
    public int Id;
    public string CardName;
    public int Cost;
    public int Power;
    public int PV;
    public string CardDescription;
    [Range(1, 4)]
    public int Rarete;
    public int Type;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI PVText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI typeText;

    public Sprite sprite;
    public Image image;
    public Image imageRareteColor;

    public InfosPanelCardCollection PanelInfos;

	void Start()
    {
        thisCard = CardDataBase.cardList[thisId];
    }

    // Update is called once per frame
    void Update()
    {
        thisCard = CardDataBase.cardList[thisId];
        Id = thisCard.Id;
        CardName = thisCard.CardName;
        Cost = thisCard.Cost;
        Power = thisCard.Power;
        PV = thisCard.PV;
        CardDescription = thisCard.CardDescription;
        Rarete = thisCard.Rarete;
        Type = thisCard.Type;

        sprite = thisCard.Image;

        nameText.text = "" + CardName;
        costText.text = "" + Cost;
        powerText.text = "" + Power;
        PVText.text = "" + PV;
        descriptionText.text = " " + CardDescription;

        image.sprite = sprite;

        if (Rarete == 0) { imageRareteColor.color = new Color(0.27f, 0.80f, 0.52f); }
        if (Rarete == 1) { imageRareteColor.color = Color.gray; }
        if (Rarete == 2) { imageRareteColor.color = new Color(0.23f, 0.43f, 0.65f); }
        if (Rarete == 3) { imageRareteColor.color = new Color(0.3f, 0.141f, 0.3f); }
        if (Rarete == 4) { imageRareteColor.color = new Color(1, 0.4f, 0); }

        switch (Type)
        {
            case 7:
                typeText.text = "Aérien";
                break;
            case 6:
                typeText.text = "Aquatique";
                break;
            case 5:
                typeText.text = "Orc";
                break;
            case 4:
                typeText.text = "Bête";
                break;
            case 3:
                typeText.text = "Nain";
                break;
            case 2:
                typeText.text = "Elfe";
                break;
            case 1:
                typeText.text = "Humain";
                break;
            default:
                Debug.Log("Error : Incorrect type of card");
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("PointerClick");
        PanelInfos.gameObject.SetActive(true);
        PanelInfos.thisCard = this.thisCard;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("PointerUp");
    }
}
