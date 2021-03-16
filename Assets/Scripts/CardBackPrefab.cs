using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBackPrefab : MonoBehaviour
{
    public GameObject Deck;
    public GameObject It;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Deck = GameObject.Find("DeckPanel");
        It.transform.SetParent(Deck.transform);
        It.transform.position = new Vector3(transform.position.x, transform.position.y, -48);
    }
}
