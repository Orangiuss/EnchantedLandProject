using System;
using System.Collections.Generic;

[Serializable] 
public class UserInGame
{
    public string localIdEnnemy;
    public string turn;
    public int hp;
    public int tailleMain;
    public int tailleDeck;
    public int deckId;
    public int heroId;

    public UserInGame()
    {
        hp = 30;
        tailleMain = 4;
        tailleDeck = 30;
        deckId = 1;
        heroId = 1;
    }

}
