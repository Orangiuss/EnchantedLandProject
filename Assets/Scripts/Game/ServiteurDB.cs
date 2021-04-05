using System;
using System.Collections.Generic;

[Serializable]
public class ServiteurDB
{
    public int id;
    public int hp;
    public int pa;

    public ServiteurDB()
    {
        id = 0;
        hp = 0;
        pa = 0;
    }

    public ServiteurDB(int id, int hp, int pa)
	{
        this.id = id;
        this.hp = hp;
        this.pa = pa;
    }
}
