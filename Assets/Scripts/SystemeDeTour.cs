using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemeDeTour : MonoBehaviour
{
    public static bool isYourTurn;
    public bool yourTurn;
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
        isYourTurn = false;
        yourTurn = false;
        yourOppenentTurn = 0;

        maxMana = 0;
        currentMana = 0;

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
        YourTurn();
        if(isYourTurn == true)
		{
            turnText.text = "Votre tour";
		} 
        else
		{
            turnText.text = "Tour de l'ennemi";
        }
        
        manaText.text = currentMana + "/" + maxMana;
        ResetManasObjects();
    }

    public void YourTurn()
    {
        if (isYourTurn == true)
        {
            if(yourTurn == false)
			{
                yourTurn = true;
                if(maxMana != 12) { maxMana++; }
                currentMana = maxMana;
                startTurn = true;
            }
        }
        if(isYourTurn == false)
		{
            yourTurn = false;
		}
    }

    public void EndYourOppenentTurn()
    {
        if (isYourTurn == false)
        {
            isYourTurn = true;

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
