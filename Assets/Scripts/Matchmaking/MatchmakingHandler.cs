using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchmakingHandler : MonoBehaviour
{
    private string databaseURL = "https://enchantedland-ae7db-default-rtdb.firebaseio.com/matchmaking/";
    private string databaseGamesURL = "https://enchantedland-ae7db-default-rtdb.firebaseio.com/games/";

    private User user = new User();
    
    public DeckCollection deck = new DeckCollection();
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("localIdPlayer") != null)
        {
            user.localId = PlayerPrefs.GetString("localIdPlayer"); /*
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

    public void MatchMaking()
    {
        LancerMatchmaking();
    }

    private void LancerMatchmaking()
    {
        User userMatch = new User();
        RestClient.Get<User>(url: databaseURL + "0.json?auth=" + PlayerPrefs.GetString("IdTokenPlayer")).Then(onResolved: response =>
        {
            userMatch = response;
            RestClient.Delete(url: databaseURL + "0.json?auth=" + PlayerPrefs.GetString("IdTokenPlayer")).Then(onResolved: response2 =>
            {
                Debug.Log(user.nomUtilisateur + " : " + user.email + ", " + user.localId + ", " + user.motDePasse);
                UserInGame userLocal = new UserInGame();
                UserInGame userEnnemy = new UserInGame();
                userLocal.localIdEnnemy = userMatch.localId;
                userEnnemy.localIdEnnemy = user.localId;
                int rand = Random.Range(0, 1);
                Debug.Log(rand);
                if (rand == 0)
                {
                    userLocal.turn = user.localId;
                    userEnnemy.turn = user.localId;
                }
                else
                {
                    userLocal.turn = userMatch.localId;
                    userEnnemy.turn = userMatch.localId;
                }
                RestClient.Put(url: databaseGamesURL + user.localId + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"), userLocal);
                RestClient.Put(url: databaseGamesURL + userMatch.localId + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"), userEnnemy);
                SceneManager.LoadScene("Game");
            }).Catch(error =>
            {
                Debug.Log(error);
            });
        }).Catch(error =>
        {
            Debug.Log(error);
            RestClient.Put(url: databaseURL + "0" + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"), user);
            StartCoroutine(MatchMakingObserver());
        });
    }

    private IEnumerator MatchMakingObserver()
    {
        yield return new WaitForSeconds(0.2f);
        RestClient.Get<UserInGame>(url: databaseGamesURL + user.localId + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer")).Then(onResolved: response =>
        {
            SceneManager.LoadScene("Game");
        }).Catch(error =>
            {
                Debug.Log(error);
                StartCoroutine(MatchMakingObserver());
            });
    }
}
