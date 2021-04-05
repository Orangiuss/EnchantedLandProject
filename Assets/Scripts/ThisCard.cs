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

    //Effets
    public int drawXcards;
    public int dealXdamage;
    public int healXhp;

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
    public bool plateauFull;
    public bool summoned;
    public GameObject battleZone;
    public int battleZoneCount;
    public Deck deck;

    public Plateau plateau;
    public GameHandler gameHandler;

    private int nombreOfBetes;
    // Start is called before the first frame update
    void Start()
    {
        plateauFull = false;
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        thisCard = CardDataBase.cardList[thisId];
        numberOfCardsInDeck = Deck.deckSize;

        canBeSummon = false;
        summoned = false;
        battleZone = GameObject.Find("MinionDropArea");
        Hand = GameObject.Find("Hand");
        deck = GameObject.Find("DeckPanel").GetComponent<Deck>();
        plateau = battleZone.GetComponent<Plateau>();
    }

    // Update is called once per frame
    void Update()
    {
        // A refaire la taille maximum !
        foreach(ServiteurDB serviteur in Plateau.serviteurStatic)
		{
            if(serviteur.id == 0) { plateauFull = false; break; } 
            else { plateauFull = true;  }
		}
        //battleZoneCount = battleZone.transform.childCount;

        if (this.tag == "Hand2")
        {
			thisCard = Deck.staticDeck[numberOfCardsInDeck - 1];
            Deck.staticDeck.RemoveAt(numberOfCardsInDeck - 1);
            numberOfCardsInDeck = numberOfCardsInDeck - 1;
            Deck.deckSize = Deck.deckSize - 1;
            this.tag = "Untagged";
            InitializeThisCard();
        }

        if (SystemeDeTour.currentMana >= Cost && summoned == false && SystemeDeTour.isYourTurn && !plateauFull)
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

        if(summoned == false && this.transform.parent == battleZone.transform)
		{
            Summon();
		}

        if(CardName == "Iris, combattante renarde")
		{
            nombreOfBetes = 0;
            foreach (ServiteurDB serviteur in Plateau.serviteurStatic)
            {
                if (CardDataBase.cardList[serviteur.id].Type == 4)
                {
                    nombreOfBetes++;
                }
            }
            if(Cost != 12 - nombreOfBetes)
			{
                Cost = 12 - nombreOfBetes;
                costText.text = "" + Cost;
            }
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
        for (int i = 0; i < 7; i++)
        {
            if(Plateau.serviteurStatic[i].id == 0)
			{
                Plateau.serviteurStatic[i].id = thisCard.Id;
                Plateau.serviteurStatic[i].hp = thisCard.PV;
                Plateau.serviteurStatic[i].pa = thisCard.Power;
                i = 7;
            }
        }
        
        // Demoniste Orc :
        if(CardName == "Demoniste gobelin")
		{
            for (int i = 0; i < 7; i++)
            {
                if (Plateau.serviteurStatic[i].id == 0)
                {
                    Plateau.serviteurStatic[i].id = thisCard.Id + 1;
                    Plateau.serviteurStatic[i].hp = 1;
                    Plateau.serviteurStatic[i].pa = 1;
                    i = 7;
                }
            }
            for (int i = 0; i < 7; i++)
            {
                if (Plateau.serviteurStatic[i].id == 0)
                {
                    Plateau.serviteurStatic[i].id = thisCard.Id + 1;
                    Plateau.serviteurStatic[i].hp = 1;
                    Plateau.serviteurStatic[i].pa = 1;
                    i = 7;
                }
            }
        }

        // Enfant enchanteur
        if (CardName == "Enfant enchanteur")
        {
            foreach (ServiteurDB serviteur in Plateau.serviteurStatic)
            {
                if (CardDataBase.cardList[serviteur.id].Type == 2 && serviteur.id != thisCard.Id)
                {
                    serviteur.hp = serviteur.hp + 2;
                    serviteur.pa = serviteur.pa + 1;
                }
            }
        }

        // Reine naine
        if (CardName == "Reine naine")
        {
            foreach (ServiteurDB serviteur in Plateau.serviteurStatic)
            {
                if (CardDataBase.cardList[serviteur.id].Type == 3 && serviteur.id != thisCard.Id)
                {
                    serviteur.pa = serviteur.pa + 2;
                }
            }
        }

        // Tueur orc 
        if (CardName == "Tueur orc")
        {
            foreach (ServiteurDB serviteur in PlateauEnnemi.serviteurEnnemiStatic)
            {
                serviteur.hp = serviteur.hp - 1;
                if(serviteur.hp <= 0) { serviteur.id = 0; }
            }
        }


        deck.Pioche(drawXcards);
        //PlayerHp.staticHp = PlayerHp.staticHp + healXhp;
        //if(PlayerHp.staticHp > 30) { PlayerHp.staticHp = 30;  }
        plateau.UpdatePlateau();
        gameHandler.UpdatePlateaus();
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

        drawXcards = thisCard.drawXcards;
        dealXdamage = thisCard.dealXdamage;
        healXhp = thisCard.healXhp;

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
