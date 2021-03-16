using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToHand : MonoBehaviour
{
    public GameObject Hand;
    public GameObject It;
    // Start is called before the first frame update
    void Start()
    {
        Hand = GameObject.Find("Hand");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag != "Drag" && this.tag != "MinionDropArea")
        {
            It.transform.SetParent(Hand.transform);
        }
    }
}
