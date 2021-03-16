using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfosPanelCardCollection : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    public Card thisCard;
    public ThisCardCollection card;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        card.thisId = thisCard.Id;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("PointerClick" + this.name);
        this.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown" + this.name);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("PointerUp" + this.name);
    }
}
