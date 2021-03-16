using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
	public static List<Card> cardList = new List<Card>();

	private void Awake()
	{
		// Carte "nulle" :
		cardList.Add(new Card(0, "", 0, 0, 0, "", Resources.Load<Sprite>("1"), 1, 1));

		// Carte de base (rarete 0) : 

		cardList.Add(new Card(1, "Nain", 1, 5, 3, "Attention à ma grosse hache", Resources.Load<Sprite>("Nain"), 2, 3));
		cardList.Add(new Card(2, "Elfe", 1, 5, 3, "Je tire à l'arc !", Resources.Load<Sprite>("2"), 3, 2));
		cardList.Add(new Card(3, "Orc", 1, 5, 3, "Rrrrrr !", Resources.Load<Sprite>("Orc"), 0, 5));
		cardList.Add(new Card(4, "Mercenaire perdu", 3, 3, 3, "", Resources.Load<Sprite>("4"), 1, 1));
		cardList.Add(new Card(5, "Kraken", 6, 3, 7, "+2 PA si vous avez 2 autres serviteurs aquatiques", Resources.Load<Sprite>("Kraken"), 4, 6));
		cardList.Add(new Card(6, "Radluir double-hache", 3, 3, 3, "", Resources.Load<Sprite>("Nain"), 1, 3));
		cardList.Add(new Card(7, "Griffon", 5, 5, 6, "", Resources.Load<Sprite>("Griffon"), 2, 7));
		cardList.Add(new Card(8, "Perdu2", 3, 3, 3, "", Resources.Load<Sprite>("4"), 1, 1));
		cardList.Add(new Card(9, "Perdu3", 3, 3, 3, "", Resources.Load<Sprite>("4"), 1, 1));
		cardList.Add(new Card(10, "Perdu4", 3, 3, 3, "", Resources.Load<Sprite>("4"), 1, 1));
		cardList.Add(new Card(11, "Perdu5", 3, 3, 3, "", Resources.Load<Sprite>("4"), 1, 1));
		cardList.Add(new Card(12, "Perdu6", 3, 3, 3, "", Resources.Load<Sprite>("4"), 1, 1));
	}
}
