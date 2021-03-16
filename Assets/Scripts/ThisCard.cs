using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ThisCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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


    public GameObject Hand;
    public int numberOfCardsInDeck;

    public bool canBeSummon;
    public bool summoned;
    public GameObject battleZone;
    public Deck deck;

    public GameObject Serviteur;
    // Start is called before the first frame update
    void Start()
    {
        thisCard = CardDataBase.cardList[thisId];
        numberOfCardsInDeck = Deck.deckSize;

        canBeSummon = false;
        summoned = false;
    }

    // Update is called once per frame
    void Update()
    {
        Hand = GameObject.Find("Hand");

        if (this.tag == "Hand2")
        {
            thisCard = Deck.staticDeck[numberOfCardsInDeck - 1];
            numberOfCardsInDeck = numberOfCardsInDeck - 1;
            Deck.deckSize = Deck.deckSize - 1;
            this.tag = "Untagged";
            InitializeThisCard();
        }

        if (SystemeDeTour.currentMana >= Cost && summoned == false)
        {
            canBeSummon = true;
        }
        else
		{
            canBeSummon = false;
		}
        if(canBeSummon == true && this.tag != "Deck")
		{
            gameObject.GetComponent<Draggable>().enabled = true;
		}
        else
		{
            gameObject.GetComponent<Draggable>().enabled = false;
        }

        battleZone = GameObject.Find("MinionDropArea");

        if(summoned == false && this.transform.parent == battleZone.transform)
		{
            Summon();
		}

    }

	public void OnPointerEnter(PointerEventData eventData)
	{
        if (this.tag != "Deck")
        {
            this.transform.localScale = new Vector3(2.3f, 2.3f, 2.3f);
            this.transform.position = new Vector3(transform.position.x, transform.position.y + 120, transform.position.z);
        }
	}

	public void OnPointerExit(PointerEventData eventData)
	{
        if (this.tag != "Deck")
        {
            this.transform.localScale = new Vector3(2, 2, 2);
            this.transform.position = new Vector3(transform.position.x, transform.position.y - 120, transform.position.z);
        }
    }

    public void Summon()
	{
        SystemeDeTour.currentMana = SystemeDeTour.currentMana - Cost;
        GameObject serviteur = Instantiate(Serviteur, transform.position, transform.rotation);
        Serviteur s = serviteur.GetComponent<Serviteur>();
        s.thisId = thisCard.Id;
        Destroy(this.gameObject);
    }

    public void InitializeThisCard()
	{
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
}
