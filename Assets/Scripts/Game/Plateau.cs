using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateau : MonoBehaviour
{
    public static List<ServiteurDB> serviteurStatic = new List<ServiteurDB>();
    public List<ServiteurDB> serviteur = new List<ServiteurDB>();

    public Serviteur[] serviteurs;

    public GameHandler gameHandler;
    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();

        serviteurStatic.Add(new ServiteurDB());
        serviteurStatic.Add(new ServiteurDB());
        serviteurStatic.Add(new ServiteurDB());
        serviteurStatic.Add(new ServiteurDB());
        serviteurStatic.Add(new ServiteurDB());
        serviteurStatic.Add(new ServiteurDB());
        serviteurStatic.Add(new ServiteurDB());
        this.serviteurs = this.gameObject.GetComponentsInChildren<Serviteur>();
        foreach (Serviteur serviteur in serviteurs)
        {
            serviteur.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        serviteur = serviteurStatic;
    }

    public void UpdatePlateau()
    {
        if (!GameHandler.wait)
        {
            for (int i = 0; i < serviteurs.Length; i++)
            {
                if (serviteurStatic[i].id != 0 && serviteurs[i].thisId != serviteurStatic[i].id)
                {
                    serviteurs[i].gameObject.SetActive(true);
                    serviteurs[i].thisId = serviteurStatic[i].id;
                    serviteurs[i].PV = serviteurStatic[i].hp;
                    serviteurs[i].Power = serviteurStatic[i].pa;
                    serviteurs[i].index = i;
                    serviteurs[i].Initialize();
                }
                else
                {
                    if (serviteurStatic[i].id == 0)
                    {
                        serviteurs[i].gameObject.SetActive(false);
                    }
                    if (serviteurStatic[i].pa != serviteurs[i].Power || serviteurStatic[i].hp != serviteurs[i].PV)
                    {
                        serviteurs[i].PV = serviteurStatic[i].hp;
                        serviteurs[i].Power = serviteurStatic[i].pa;

                        serviteurs[i].powerText.text = "" + serviteurs[i].Power;
                        serviteurs[i].PVText.text = "" + serviteurs[i].PV;
                    }
                }
            }
        }
    }
}
