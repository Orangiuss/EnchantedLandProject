using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("OnPointerEnter");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("OnPointerExit");
	}

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log("Drop on" + gameObject.name);


		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

		eventData.pointerDrag.tag = gameObject.name;
		if (d != null)
		{
			d.parentToReturnTo = this.transform;
		}
	}
}
