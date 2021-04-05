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

    public bool directAttack;
    public int drawXcards;
    public int dealXdamage;
    public int healXhp;

    public Sprite Image;

    public Card()
	{

	}

    public Card(int id, string cardName, int cost, int power, int pv, string cardDescription, Sprite image, int rarete, int type, bool attack, int drawXcards = 0, int dealXdamage = 0, int healXhp = 0)
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
        directAttack = attack;
        this.drawXcards = drawXcards;
        this.dealXdamage = dealXdamage;
        this.healXhp = healXhp;
	}
}
