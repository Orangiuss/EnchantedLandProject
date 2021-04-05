using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedDeck : MonoBehaviour, IPointerClickHandler
{
	public GameObject bordure;
	public static int x;
	public static SelectedDeck selectedDeck;

	void Start()
	{
		bordure.SetActive(false);
		x = 0; 
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if(x == 0)
		{
			x = 1;
			selectedDeck = this;
		}
		if(x == 1)
		{
			selectedDeck.bordure.SetActive(false);
			selectedDeck = this;
		}
		bordure.SetActive(true);
		Debug.Log(selectedDeck.GetComponent<DeckObject>().deckId);
	}
}
