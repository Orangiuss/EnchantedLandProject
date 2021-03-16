using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
	{
		SceneManager.LoadScene("ChoixDeck");
	}

	public void Collection()
	{
		SceneManager.LoadScene("Collection");
	}

	public void Boutique()
	{
		SceneManager.LoadScene("Boutique");
	}

	public void QuitGame()
	{
		Debug.Log("QUIT !!");
		Application.Quit();
	}

	public void Deconnexion()
	{
		SceneManager.LoadScene("RegisterLogin");
	}
}
