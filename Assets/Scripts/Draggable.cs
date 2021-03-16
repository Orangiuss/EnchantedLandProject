using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	public float x;
	public float y;
	public Transform parentToReturnTo = null;

	GameObject placeholder = null;

	public void OnBeginDrag(PointerEventData eventData)
	{
		placeholder = new GameObject();
		placeholder.transform.SetParent(this.transform.parent);
		LayoutElement le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.flexibleHeight = 0;
		le.flexibleWidth = 0;

		placeholder.transform.SetSiblingIndex( this.transform.GetSiblingIndex() );

		parentToReturnTo = this.transform.parent;
		this.transform.SetParent(this.transform.parent.parent);
		this.tag = "Drag";

		x = eventData.position.x - this.transform.position.x;
		y = eventData.position.y - this.transform.position.y;

		GetComponent<CanvasGroup>().blocksRaycasts = false;
		Debug.Log("OnBeginDrag");
	}

	public void OnDrag(PointerEventData eventData)
	{
		//Debug.Log("OnDrag");

		this.transform.position = new Vector3(eventData.position.x - x, eventData.position.y - y);
	}

	public void OnEndDrag(PointerEventData eventData)
	{

		GetComponent<CanvasGroup>().blocksRaycasts = true;
		this.transform.SetParent(parentToReturnTo);
		this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
		Debug.Log("OnEndDrag");

		Destroy(placeholder);
	}
}
