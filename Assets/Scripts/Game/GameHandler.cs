using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private string databaseURL = "https://enchantedland-ae7db-default-rtdb.firebaseio.com/games/";
    private string databaseCollectionURL = "https://enchantedland-ae7db-default-rtdb.firebaseio.com/collections/";

    private string IdPlayer;
    private string IdPlayerEnnemi;
    private string player;
    //private string playerEnnemi;
    private string gametoken;

    private Game game = new Game();

    private UserInGame user = new UserInGame();
    private UserInGame userPut = new UserInGame();
    private UserInGame userEnnemy = new UserInGame();
    private UserInGame userPutEnnemy = new UserInGame();

    public Plateau plateau;
    public PlateauEnnemi plateauEnnemi;
    public GameObject HandEnnemi;
    public RectTransform[] CardsEnnemi;

    public GameObject Hand;
    private int cardsInHand;

    public DeckCollection deckCollection = new DeckCollection();

    public List<Card> deckCard = new List<Card>();
    public static bool wait;

    private bool finishGame;
    public GameObject panelVictory;
    public TextMeshProUGUI victoryText;
    // Start is called before the first frame update
    void Start()
    {
        finishGame = false;
        // On recupere le game token de la partie
        gametoken = PlayerPrefs.GetString("gametoken");
        databaseURL = databaseURL + gametoken + "/";
        wait = false;
        IdPlayer = PlayerPrefs.GetString("localIdPlayer");
        IdPlayerEnnemi = PlayerPrefs.GetString("ennemiIdPlayer");
        player = PlayerPrefs.GetString("player");
        //if(player.Equals("player1")) { playerEnnemi = "player2";  } else { playerEnnemi = "player1"; }
        DataFromDataBase();
        DeckFromDataBase();
        HandEnnemi = GameObject.Find("HandEnnemi");
        Hand = GameObject.Find("Hand");
        panelVictory = GameObject.Find("EndGamePanel");
        panelVictory.SetActive(false);
        cardsInHand = Hand.transform.childCount;
        CardsEnnemi = HandEnnemi.GetComponentsInChildren<RectTransform>();
        EnnemiUpdate(5);
        StartCoroutine(PutToDataBase());
        StartCoroutine(DataBase());
    }

    // Update is called once per frame
    void Update()
    {
        if (!finishGame)
        {
            EnnemiUpdate(userEnnemy.tailleMain);
            if (PlayerHp.staticHp <= 0)
            {
                finishGame = true;
                victoryText.color = Color.red;
                victoryText.text = "Défaite !";
                panelVictory.SetActive(true);
            }
            if (EnnemyHp.staticHp <= 0)
            {
                finishGame = true;
                victoryText.color = Color.green;
                victoryText.text = "Victoire !";
                panelVictory.SetActive(true);
            }
        }
    }

    private IEnumerator DataBase()
	{
        yield return new WaitForSeconds(0.2f);
        DataFromDataBase();
        StartCoroutine(DataBase());
    }

    private IEnumerator PutToDataBase()
    {
        yield return new WaitForSeconds(0.5f);
        if (!wait && SystemeDeTour.isYourTurn && !finishGame)
        {
            cardsInHand = Hand.transform.childCount;
            userPut = user;
            userPut.tailleMain = cardsInHand;
            userPut.hp = PlayerHp.staticHp;
            userPut.tailleDeck = Deck.deckSize;
            RestClient.Put(url: databaseURL + player + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"), userPut);
        }
        StartCoroutine(PutToDataBase());
    }

    private void DataFromDataBase()
    {
        RestClient.Get<Game>(url: databaseURL + ".json").Then(onResolved: response =>
        {
            if (!wait && !finishGame)
            {
                game = response;
                if (player.Equals("player1"))
                {
                    user = game.player1;
                    userEnnemy = game.player2;
                }
                else
                {
                    user = game.player2;
                    userEnnemy = game.player1;
                }
                if (player.Equals("player1"))
                {
                    PlateauEnnemi.serviteurEnnemiStatic = game.plateau2;
                    Plateau.serviteurStatic = game.plateau1;
                }
                else
                {
                    PlateauEnnemi.serviteurEnnemiStatic = game.plateau1;
                    Plateau.serviteurStatic = game.plateau2;
                }

                if (game.turn.Equals(PlayerPrefs.GetString("localIdPlayer")))
                {
                    SystemeDeTour.isYourTurn = true;
                }
                if (SystemeDeTour.isYourTurn) { userPut.turn = userEnnemy.localIdEnnemy; }
                else { userPut.turn = user.localIdEnnemy; }

                PlayerPrefs.SetString("ennemiIdPlayer", user.localIdEnnemy);
            }
        }).Catch(error =>
        {
            Debug.Log(error);
            // Le joueur n'est pas dans une partie
        });
    }

    public void EndTurn()
	{
        wait = true;
        game.turn = IdPlayerEnnemi;
        SystemeDeTour.isYourTurn = false;
        if (player.Equals("player1"))
        {
            game.plateau1 = Plateau.serviteurStatic;
            game.plateau2 = PlateauEnnemi.serviteurEnnemiStatic;
        }
        else
        {
            game.plateau2 = Plateau.serviteurStatic;
            game.plateau1 = PlateauEnnemi.serviteurEnnemiStatic;
        }
        RestClient.Put(url: databaseURL + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"), game).Then(onResolved: response =>
        {
            wait = false;
        });
     }

    public void UpdatePlateaus()
	{
        wait = true;
        userPut = user;
        userPutEnnemy = userEnnemy;
        userPutEnnemy.hp = EnnemyHp.staticHp;
        userPut.tailleMain = cardsInHand;
        userPut.hp = PlayerHp.staticHp;
        userPut.tailleDeck = Deck.deckSize;
        if (player.Equals("player1"))
        {
            game.plateau1 = Plateau.serviteurStatic;
            game.plateau2 = PlateauEnnemi.serviteurEnnemiStatic;
            game.player1 = userPut;
            game.player2 = userPutEnnemy;
        }
        else
        {
            game.plateau2 = Plateau.serviteurStatic;
            game.plateau1 = PlateauEnnemi.serviteurEnnemiStatic;
            game.player1 = userPutEnnemy;
            game.player2 = userPut;
        }
        RestClient.Put(url: databaseURL + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"), game).Then(onResolved: response =>
        {
            wait = false;
        });
    }

    private void EnnemiUpdate(int n)
    {
        if (!wait)
        {
            foreach (RectTransform card in CardsEnnemi)
            {
                card.gameObject.SetActive(false);
            }
            for (int i = 0; i < n + 1; i++)
            {
                CardsEnnemi[i].gameObject.SetActive(true);
            }
            DeckEnnemy.deckSize = userEnnemy.tailleDeck;
            EnnemyHp.staticHp = userEnnemy.hp;
            PlayerHp.staticHp = user.hp;
        plateau.UpdatePlateau();
        plateauEnnemi.UpdatePlateau();
        }
    }

    private void DeckFromDataBase()
    {
        string localId = PlayerPrefs.GetString("localIdPlayer");
        Card card = new Card();
        RestClient.Get<Collection>(url: databaseCollectionURL + localId + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer")).Then(onResolved: response =>
        {
            deckCollection = response.decks[user.deckId-1];
            for (int i = 0; i < deckCollection.deck.Count; i++)
            {
                card = CardDataBase.cardList[deckCollection.deck[i]];
                deckCard.Add(card);
            }
            Deck.staticDeck = deckCard;
            Deck.deckSize = deckCard.Count;
            Deck.startGame = true;
        }).Catch(error =>
        {
            Debug.Log(error);
        });
    }

    public void Menu()
    {
        RestClient.Delete(url: databaseURL + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"));
        SceneManager.LoadScene("Menu");
    }
}
