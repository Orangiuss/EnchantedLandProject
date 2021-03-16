using System;

[Serializable] 
public class UserInGame
{
    public string localIdEnnemy;
    public string turn;
    public int hp;
    public int tailleMain;
    public int tailleDeck;
    public DeckCollection deck;

    public UserInGame()
    {
        hp = 30;
        tailleMain = 5;
        tailleDeck = 30;
    }

}
