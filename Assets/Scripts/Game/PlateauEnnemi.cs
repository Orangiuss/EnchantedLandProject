using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateauEnnemi : MonoBehaviour
{
    public static List<ServiteurDB> serviteurEnnemiStatic = new List<ServiteurDB>();
    public List<ServiteurDB> serviteur = new List<ServiteurDB>();

    public Serviteur[] serviteurs;

    public GameHandler gameHandler;
    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();

        serviteurEnnemiStatic.Add(new ServiteurDB());
        serviteurEnnemiStatic.Add(new ServiteurDB());
        serviteurEnnemiStatic.Add(new ServiteurDB());
        serviteurEnnemiStatic.Add(new ServiteurDB());
        serviteurEnnemiStatic.Add(new ServiteurDB());
        serviteurEnnemiStatic.Add(new ServiteurDB());
        serviteurEnnemiStatic.Add(new ServiteurDB());
        this.serviteurs = this.gameObject.GetComponentsInChildren<Serviteur>();
        foreach (Serviteur serviteur in serviteurs)
		{
            serviteur.gameObject.SetActive(false);
		}
    }

    // Update is called once per frame
    void Update()
    {
        serviteur = serviteurEnnemiStatic;
    }

    public void UpdatePlateau()
	{
        if (!GameHandler.wait)
        {
            for (int i = 0; i < serviteurs.Length; i++)
            {
                if (serviteurEnnemiStatic[i].id != 0 && serviteurs[i].thisId != serviteurEnnemiStatic[i].id)
                {
                    serviteurs[i].gameObject.SetActive(true);
                    serviteurs[i].thisId = serviteurEnnemiStatic[i].id;
                    serviteurs[i].PV = serviteurEnnemiStatic[i].hp;
                    serviteurs[i].Power = serviteurEnnemiStatic[i].pa;
                    serviteurs[i].index = i;
                    serviteurs[i].Initialize();
                }
                else
                {
                    if (serviteurEnnemiStatic[i].id == 0)
                    {
                        serviteurs[i].gameObject.SetActive(false);
                    }
                    if (serviteurEnnemiStatic[i].pa != serviteurs[i].Power || serviteurEnnemiStatic[i].hp != serviteurs[i].PV)
                    {
                        serviteurs[i].PV = serviteurEnnemiStatic[i].hp;
                        serviteurs[i].Power = serviteurEnnemiStatic[i].pa;

                        serviteurs[i].powerText.text = "" + serviteurs[i].Power;
                        serviteurs[i].PVText.text = "" + serviteurs[i].PV;
                    }
                }
            }
        }
	}
}
