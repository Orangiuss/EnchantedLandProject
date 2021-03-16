using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshToken : MonoBehaviour
{
    private string AuthKey = "AIzaSyD_kvrwhmg8uQCSNFmZYhzR4lHpDoTZkrA";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Refresh());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Refresh()
    {
        yield return new WaitForSeconds(60);
        string userData = "{\"grant_type\":\"refresh_token\",\"refresh_token\":\"" + PlayerPrefs.GetString("RefreshToken") + "\"}";
        Debug.Log(userData);
        RestClient.Post<RefreshResponse>("https://securetoken.googleapis.com/v1/token?key=" + AuthKey, userData).Then(
            response =>
            {
                Debug.Log(response.id_token);
                PlayerPrefs.SetString("IdTokenPlayer", response.id_token);
                PlayerPrefs.SetString("RefreshToken", response.refresh_token);
                StartCoroutine(Refresh());
            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }
}
