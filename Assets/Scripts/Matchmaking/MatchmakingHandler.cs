using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchmakingHandler : MonoBehaviour
{
    private string databaseURL = "https://enchantedland-ae7db-default-rtdb.firebaseio.com/matchmaking/";
    private string databaseGamesURL = "https://enchantedland-ae7db-default-rtdb.firebaseio.com/games/";

    private UserInGame user = new UserInGame();

    private int deckId;
    private int heroId;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("localIdPlayer") != null)
        {
            user.turn = PlayerPrefs.GetString("localIdPlayer"); /*
            idToken = PlayerPrefs.GetString("IdTokenPlayer");
            refreshToken = PlayerPrefs.GetString("RefreshToken");
            Debug.Log("Vous etes bien connecté !");

            Debug.Log("IdToken :" + idToken);
            Debug.Log("Refresh :" + refreshToken); */
        }
        else
        {
            Debug.Log("GROSSE ERREUR !");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RetourneMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void MatchMaking()
    {
        if (SelectedDeck.selectedDeck != null)
        {
            LancerMatchmaking();
        }
    }

    private void LancerMatchmaking()
    {
        user.deckId = SelectedDeck.selectedDeck.GetComponent<DeckObject>().deckId;
        user.heroId = SelectedDeck.selectedDeck.GetComponent<DeckObject>().heroId;
        string gametoken = CreateRandomString();
        user.localIdEnnemy = gametoken;
        UserInGame userMatch = new UserInGame();
        RestClient.Get<UserInGame>(url: databaseURL + "0.json?auth=" + PlayerPrefs.GetString("IdTokenPlayer")).Then(onResolved: response =>
        {
            userMatch = response;
            PlayerPrefs.SetString("gametoken", userMatch.localIdEnnemy);
            RestClient.Delete(url: databaseURL + "0.json?auth=" + PlayerPrefs.GetString("IdTokenPlayer")).Then(onResolved: response2 =>
            {
                UserInGame userLocal = new UserInGame();
                UserInGame userEnnemy = new UserInGame();
                userLocal = user;
                userEnnemy = userMatch;
                userLocal.localIdEnnemy = userMatch.turn;
                userEnnemy.localIdEnnemy = user.turn;
                PlayerPrefs.SetString("ennemiIdPlayer", userLocal.localIdEnnemy);
                int rand = Random.Range(0, 1);
                Debug.Log(rand);
                if (rand == 0)
                {
                    userLocal.turn = user.turn;
                    userEnnemy.turn = user.turn;
                }
                else
                {
                    userLocal.turn = userMatch.turn;
                    userEnnemy.turn = userMatch.turn;
                }
                string turn = userLocal.turn;
                Game game = new Game();
                game.turn = turn;
                game.player1 = userLocal;
                game.player2 = userEnnemy;
                PlayerPrefs.SetString("player", "player1");
                RestClient.Put(url: databaseGamesURL + PlayerPrefs.GetString("gametoken") + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"), game).Then(onResolved: response5 =>
                {
                    SceneManager.LoadScene("Game");
                });
            }).Catch(error =>
            {
                Debug.Log(error);
            });
        }).Catch(error =>
        {
            Debug.Log(error);
            PlayerPrefs.SetString("gametoken", gametoken);
            RestClient.Put(url: databaseURL + "0" + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"), user);
            PlayerPrefs.SetString("player", "player2");
            StartCoroutine(MatchMakingObserver(gametoken));
        });
    }

    private IEnumerator MatchMakingObserver(string gametoken)
    {
        yield return new WaitForSeconds(0.05f);
        RestClient.Get<Game>(url: databaseGamesURL + gametoken + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer")).Then(onResolved: response =>
        {
            SceneManager.LoadScene("Game");
        }).Catch(error =>
        {
            Debug.Log(error);
            StartCoroutine(MatchMakingObserver(gametoken));
        });
    }

    private string CreateRandomString(int stringLength = 30)
    {
        int _stringLength = stringLength - 1;
        string randomString = "";
        string[] characters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        for (int i = 0; i <= _stringLength; i++)
        {
            randomString = randomString + characters[Random.Range(0, characters.Length)];
        }
        return randomString;
    }
}
