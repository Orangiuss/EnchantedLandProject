using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Serviteur : MonoBehaviour
{
    public Card thisCard = new Card();
    public int thisId;
    public int Id;
    public int type;
    public int Cost;
    public int Power;
    public int PV;

    public Sprite sprite;
    public Image image;

    public TextMeshProUGUI typeText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI PVText;

    public GameObject MinionDropArea;
    // Start is called before the first frame update
    void Start()
    {
        MinionDropArea = GameObject.Find("MinionDropArea");
        this.transform.SetParent(MinionDropArea.transform);
        thisCard = CardDataBase.cardList[thisId];
    }

	// Update is called once per frame
	void Update()
    {
        Id = thisCard.Id;
        type = thisCard.Type;
        Cost = thisCard.Cost;
        Power = thisCard.Power;
        PV = thisCard.PV;

        sprite = thisCard.Image;

        powerText.text = "" + Power;
        PVText.text = "" + PV;

        switch (type)
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

        image.sprite = sprite;
    }
}
