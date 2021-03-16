using Proyecto26;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataBaseHandler : MonoBehaviour
{
	private string databaseURL = "https://enchantedland-ae7db-default-rtdb.firebaseio.com/users/";
	private string databaseURLCollection = "https://enchantedland-ae7db-default-rtdb.firebaseio.com/collections/";
	private string AuthKey = "AIzaSyD_kvrwhmg8uQCSNFmZYhzR4lHpDoTZkrA";

	private string clientIdGoogle = "643098867066-nndhhv6u00n8h9h7n3pste6t87rjfcoh.apps.googleusercontent.com";
	private string secretIdGoogle = "jFAu-fssf8gdkiMAQjYmYurT";

	private string idToken;
	public static string localId;

	public InputField nomUtilisateurI;
	public InputField email;
	public InputField motDePasseI;
	public InputField motDePasse2;

	public InputField emailC;
	public InputField motDePasseC;

	public InputField emailMDR;

	public InputField googleCode;

	public void Inscription()
	{
		if (motDePasseI.text == motDePasse2.text)
		{
			User user = new User(nomUtilisateurI.text, email.text, motDePasseI.text, "0", "0");
			InscriptionDataBase(user);
		}
		else
		{
			Debug.Log("Pas les memes mots de passe");
		}
	}

	public void Connexion()
	{
		if (emailC.text != "Adresse email" && motDePasseC.text != "Mot de passe")
		{
			User user = new User("", emailC.text, motDePasseC.text, "0", "0");
			user.email = emailC.text;
			user.motDePasse = motDePasseC.text;
			ConnexionDataBase(user);
		}
	}

	public void ConnexionGoogle()
	{
		ConnexionGoogleDataBase(idToken =>
		{
			SignInWithToken(idToken, "google.com");
		});
	}

	public void EnvoiMailRecuperation()
	{
		if (emailMDR != null)
		{
			EnvoiMDRDataBase(emailMDR.text);
		}
	}

	/*
	public void Modifier(User user)
	{
		RestClient.Delete(url: databaseURL + user.localId + ".json").Then(onResolved: response2 =>
		{
			RestClient.Put(url: databaseURL + user.localId + ".json", user);
		});
		Debug.Log(user.nomUtilisateur + " : " + user.email + ", " + user.localId + ", " + user.motDePasse);
	} */

	private void PostToDataBase(User user)
	{
		RestClient.Put(url: databaseURL + user.localId + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"), user);
	}

	private void PostToDataBaseNewCollection(string localIdUser)
	{
		Collection collection = new Collection();
		RestClient.Put(url: databaseURLCollection + localIdUser + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer"), collection);
	}

	private void RetrieveFromDataBase(User user, string localId)
	{
		RestClient.Get<User>(url: databaseURL + localId + ".json?auth=" + PlayerPrefs.GetString("IdTokenPlayer")).Then(onResolved: response =>
	   {
		   user = response;
		   Debug.Log(user.nomUtilisateur + " : " + user.email + ", " + user.localId + ", " + user.motDePasse);
	   });
	}

	private void InscriptionDataBase(User user)
	{
		string userData = "{\"email\":\"" + user.email + "\",\"password\":\"" + user.motDePasse + "\",\"returnSecureToken\":true}";
		RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=" + AuthKey, userData).Then(
			response =>
			{
				user.localId = response.localId;
				Debug.Log(response.expiresIn);
				PlayerPrefs.SetString("localIdPlayer", user.localId);
				PlayerPrefs.SetString("IdTokenPlayer", response.idToken);
				PlayerPrefs.SetString("RefreshToken", response.refreshToken);
				PostToDataBase(user);
				PostToDataBaseNewCollection(user.localId);
				SceneManager.LoadScene("Menu");
			}).Catch(error =>
			{
				Debug.Log(error);
			});
	}

	private void ConnexionDataBase(User user)
	{
		string userData = "{\"email\":\"" + user.email + "\",\"password\":\"" + user.motDePasse + "\",\"returnSecureToken\":true}";
		RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + AuthKey, userData).Then(
			response =>
			{
				user.localId = response.localId;
				Debug.Log(response.expiresIn);
				PlayerPrefs.SetString("localIdPlayer", user.localId);
				PlayerPrefs.SetString("IdTokenPlayer", response.idToken);
				PlayerPrefs.SetString("RefreshToken", response.refreshToken);
				SceneManager.LoadScene("Menu");
			}).Catch(error =>
			{
				Debug.Log(error);
			});
	}

	private void EnvoiMDRDataBase(string email)
	{
		string userData = "{\"requestType\":\"PASSWORD_RESET\",\"email\":\"" + email + "\"}";
		RestClient.Post<string>("https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=" + AuthKey, userData).Then(
			response =>
			{

			}).Catch(error =>
			{
				Debug.Log(error);
			});
	}

	public void GoogleCode()
	{
		Application.OpenURL("https://accounts.google.com/o/oauth2/v2/auth?client_id={" + clientIdGoogle + "}&redirect_uri=urn:ietf:wg:oauth:2.0:oob&response_type=code&scope=email");
	}
	private void ConnexionGoogleDataBase(Action<string> callback)
	{
		RestClient.Post("https://oauth2.googleapis.com/token?code={" + googleCode.text + "}&client_id={" + clientIdGoogle + "}client_secret={" + secretIdGoogle + "}&redirect_uri=urn:ietf:wg:oauth:2.0:oob&grant_type={authorization_code}", null).Then(
			response =>
			{
				var data = StringSerializationAPI.Deserialize(typeof(GoogleTokenId), response.Text) as GoogleTokenId;
			}).Catch(error =>
			{
				Debug.Log(error);
			});
	}

	private void SignInWithToken(string idToken, string providerId)
	{
		var payLoad = "{\"postBody\":\"id_token ={" + idToken + "} & providerId ={" + providerId + "}\",\"requestUri\":\"http://localhost\",\"returnIdpCredential\":true,\"returnSecureToken\":true}";
		RestClient.Post("https://identitytoolkit.googleapis.com/v1/accounts:signInWithIdp?key=" + AuthKey, payLoad).Then(
			response =>
			{
				Debug.Log(response.Text);
			}).Catch(error =>
			{
				Debug.Log(error);
			});
	}
}
