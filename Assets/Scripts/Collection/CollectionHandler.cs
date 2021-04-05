using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Proyecto26;
using TMPro;

public class CollectionHandler : MonoBehaviour
{
    private string databaseURL = "https://enchantedland-ae7db-default-rtdb.firebaseio.com/collections/";

    public GameObject cartesCollection;
    public ThisCardCollection[] cardsList;
    public int nombreDePages;
    public int nombreDeCartes;
    public int page;
    public int nombreCartesAAfficher;

    public List<int> collectionPlayer = new List<int>();

    public GameObject moinsPage;
    public GameObject plusPage;

    public TextMeshProUGUI numeroPageText;

    public static int[] nbrCards;

    void Start()
    {
        page = 1;
        cartesCollection = GameObject.Find("Cartes");
        cardsList = cartesCollection.GetComponentsInChildren<ThisCardCollection>();
        moinsPage.SetActive(false);
        CollectionFromDataBase();
    }
    // Update is called once per frame
    void Update()
    {
        foreach (ThisCardCollection card in cardsList)
        {
            if (card.thisId == 0)
            {
                card.gameObject.SetActive(false);
            }
            else
			{
                card.gameObject.SetActive(true);
            }
        }
    }

    public void RetourneMenu()
	{
        SceneManager.LoadScene("Menu");
    }

    public void moins()
	{
        page--;
        CleanCardsList();
        plusPage.SetActive(true);
        if (page == 1)
        {
            moinsPage.SetActive(false);
        }
        if (nombreDeCartes - ((page - 1) * 10) < 10)
        {
            nombreCartesAAfficher = nombreDeCartes - ((page - 1) * 10);
        }
        else
        {
            nombreCartesAAfficher = 10;
        }
        for (int i = 0; i < nombreCartesAAfficher; i++)
        {
            cardsList[i].thisId = collectionPlayer[i + (((page -1) * 10))];
            cardsList[i].Initialize();
        }
        numeroPageText.text = page + "/" + nombreDePages;
    }

    public void plus()
    {
        page++;
        CleanCardsList();
        moinsPage.SetActive(true);
        if (page == nombreDePages)
        {
            plusPage.SetActive(false);
        }
        if (nombreDeCartes - ((page - 1) * 10) < 10)
        {
            nombreCartesAAfficher = nombreDeCartes - ((page - 1) * 10);
        }
        else
        {
            nombreCartesAAfficher = 10;
        }
        for (int i = 0; i < nombreCartesAAfficher; i++)
        {
            cardsList[i].thisId = collectionPlayer[i + (((page - 1) * 10))];
            cardsList[i].Initialize();
        }
        numeroPageText.text = page + "/" + nombreDePages;
    }

    private void CollectionFromDataBase()
    {
        string localId = PlayerPrefs.GetString("localIdPlayer");
        RestClient.Get<Collection>(url: databaseURL + localId + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer")).Then(onResolved: response =>
        {
            collectionPlayer = response.collection;
            collectionPlayer.Sort();
            nombreDeCartes = collectionPlayer.Count;
            nombreDePages = Mathf.CeilToInt(nombreDeCartes / 10f);
            if (page == 1 && nombreDePages == 1)
            {
                plusPage.SetActive(false);
            }
            numeroPageText.text = page + "/" + nombreDePages;
            if(nombreDeCartes - ((page - 1) * 10) < 10)
			{
                nombreCartesAAfficher = nombreDeCartes - ((page - 1) * 10);
			} 
            else
			{
                nombreCartesAAfficher = 10;
			}
            for (int i = 0; i < nombreCartesAAfficher; i++)
            {
                cardsList[i].thisId = collectionPlayer[i + (((page - 1) * 10))];
                cardsList[i].Initialize();
            }
        }).Catch(error =>
        {
            Debug.Log(error);
        });
    }

    private void CleanCardsList()
    {
        foreach (ThisCardCollection card in cardsList)
        {
            card.thisId = 0;
        }
    }
}
