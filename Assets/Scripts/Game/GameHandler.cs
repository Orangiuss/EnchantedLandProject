using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private string databaseURL = "https://enchantedland-ae7db-default-rtdb.firebaseio.com/games/";

    private string IdPlayer;

    private UserInGame user = new UserInGame();
    private UserInGame userEnnemy = new UserInGame();

    private GameObject HandEnnemi;
    private RectTransform[] CardsEnnemi;

    public GameObject Hand;
    private int cardsInHand;
    // Start is called before the first frame update
    void Start()
    {
        IdPlayer = PlayerPrefs.GetString("localIdPlayer");
        DataFromDataBase();
        DataEnnemyFromDataBase();
        HandEnnemi = GameObject.Find("HandEnnemi");
        Hand = GameObject.Find("Hand");
        cardsInHand = Hand.transform.childCount;
        CardsEnnemi = HandEnnemi.GetComponentsInChildren<RectTransform>();
        EnnemiHandUpdate(5);
    }

    // Update is called once per frame
    void Update()
    {
        cardsInHand = Hand.transform.childCount;
        user.tailleMain = cardsInHand;
        PutToDataBase();
        DataFromDataBase();
        DataEnnemyFromDataBase();
    }

    private void PutToDataBase()
    {
        RestClient.Put(url: databaseURL + IdPlayer + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"), user);
    }

    private void DataFromDataBase()
    {
        RestClient.Get<UserInGame>(url: databaseURL + IdPlayer + ".json").Then(onResolved: response =>
        {
            user = response;
        }).Catch(error =>
        {
            Debug.Log(error);
            // Le joueur n'est pas dans une partie
        });
    }

    private void DataEnnemyFromDataBase()
    {
        RestClient.Get<UserInGame>(url: databaseURL + user.localIdEnnemy + ".json").Then(onResolved: response =>
        {
            userEnnemy = response;
            Debug.Log(response.tailleMain);
            EnnemiHandUpdate(userEnnemy.tailleMain);
        }).Catch(error =>
        {
            Debug.Log(error);
            // Le joueur ennemi n'est pas dans une partie ou n'est pas le bon
        });
    }

    public void EndTurn()
	{
        user.turn = user.localIdEnnemy;
        userEnnemy.turn = user.localIdEnnemy;
        RestClient.Put(url: databaseURL + IdPlayer + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"), user);
        RestClient.Put(url: databaseURL + user.localIdEnnemy + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"), userEnnemy);
    }

    private void EnnemiHandUpdate(int n)
    {
        foreach(RectTransform card in CardsEnnemi)
        {
            card.gameObject.SetActive(false);
        }
        for (int i = 0; i < n+1; i++)
        {
            CardsEnnemi[i].gameObject.SetActive(true);
        }
    }
}
