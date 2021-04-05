using System;
using System.Collections.Generic;

[Serializable]
public class Game
{
    public string turn;
    public List<ServiteurDB> plateau1 = new List<ServiteurDB>();
    public List<ServiteurDB> plateau2 = new List<ServiteurDB>();
    public UserInGame player1 = new UserInGame();
    public UserInGame player2 = new UserInGame();

    public Game()
    {
        plateau1.Add(new ServiteurDB());
        plateau1.Add(new ServiteurDB());
        plateau1.Add(new ServiteurDB());
        plateau1.Add(new ServiteurDB());
        plateau1.Add(new ServiteurDB());
        plateau1.Add(new ServiteurDB());
        plateau1.Add(new ServiteurDB());
        plateau2.Add(new ServiteurDB());
        plateau2.Add(new ServiteurDB());
        plateau2.Add(new ServiteurDB());
        plateau2.Add(new ServiteurDB());
        plateau2.Add(new ServiteurDB());
        plateau2.Add(new ServiteurDB());
        plateau2.Add(new ServiteurDB());
    }
}
