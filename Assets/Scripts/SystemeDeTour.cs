using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemeDeTour : MonoBehaviour
{
    public bool isYourTurn;
    public int yourTurn;
    public int yourOppenentTurn;

    public Text turnText;
    public int maxMana;
    public static int currentMana;
    public Text manaText;

    public GameObject[] manasObjects;

    static public bool startTurn;
    // Start is called before the first frame update
    void Start()
    {
        isYourTurn = true;
        yourTurn = 1;
        yourOppenentTurn = 0;

        maxMana = 1;
        currentMana = 1;

        foreach( GameObject mana in manasObjects)
		{
            mana.SetActive(false);
		}
        manasObjects[0].SetActive(true);

        startTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isYourTurn == true)
		{
            turnText.text = "Your turn";
		} 
        else
		{
            turnText.text = "Oppenent Turn";
        }
        
        manaText.text = currentMana + "/" + maxMana;
        ResetManasObjects();
    }

    public void EndYourTurn()
	{
        if (isYourTurn == true)
        {
            isYourTurn = false;
            yourOppenentTurn++;
        }
	}

    public void EndYourOppenentTurn()
    {
        if (isYourTurn == false)
        {
            isYourTurn = true;
            yourTurn++;

            maxMana++;
            currentMana = maxMana;
            startTurn = true;
        }
    }

    public void ResetManasObjects()
	{
        foreach (GameObject mana in manasObjects)
        {
            mana.SetActive(false);
        }
        for (int i = 0; i < currentMana; i++)
        {
            manasObjects[i].SetActive(true);
        }
    }

}
