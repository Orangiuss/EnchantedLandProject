using System;

[Serializable] // This makes the class able to be serialized into a JSON
public class User
{
    public string nomUtilisateur;
    public string email;
    public string motDePasse;
    public string localId;

    public User()
	{
        this.nomUtilisateur = "";
        this.email = "";
        this.motDePasse = "";
        this.localId = "";
    }

    public User(string nomutilisateur, string email, string motDePasse, string localid, string idtoken)
    {
        this.nomUtilisateur = nomutilisateur;
        this.email = email;
        this.motDePasse = motDePasse;
        this.localId = localid;
    }
}