using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Card
{
    public int Id;
    public string CardName;
    public int Cost;
    public int Power;
    public int PV;
    public string CardDescription;
    public int Rarete;
    public int Type;

    public Sprite Image;

    public Card()
	{

	}

    public Card(int id, string cardName, int cost, int power, int pv, string cardDescription, Sprite image, int rarete, int type)
	{
        Id = id;
        CardName = cardName;
        Cost = cost;
        Power = power;
        CardDescription = cardDescription;
        PV = pv;
        Image = image;
        Rarete = rarete;
        Type = type;
	}
}
