using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBack : MonoBehaviour
{
    public bool cardBack;
    public static bool staticCardBack;

    public GameObject cardBackObject;
    public GameObject cardObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        staticCardBack = cardBack;

        if (staticCardBack == true)
        {
            cardBackObject.SetActive(true);
            cardObject.SetActive(false);
        }
        else
        {
            cardBackObject.SetActive(false);
            cardObject.SetActive(true);
        }
    }
}
