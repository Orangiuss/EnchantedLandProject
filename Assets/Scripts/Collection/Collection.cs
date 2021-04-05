using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Collection
{
    public List<int> collection = new List<int>();
    public List<DeckCollection> decks = new List<DeckCollection>();

    public Collection()
    {
        // Collection de base à ajouter
        decks.Add(new DeckCollection());
        decks.Add(new DeckCollection());
        decks.Add(new DeckCollection());
        decks.Add(new DeckCollection());
        decks.Add(new DeckCollection());
        collection.Add(1);
        collection.Add(2);
        collection.Add(3);
        collection.Add(4);
        collection.Add(5);
        collection.Add(6);
        collection.Add(7);
        collection.Add(8);
        collection.Add(9);
        collection.Add(10);
        collection.Add(11);
        collection.Add(12);
        collection.Add(13);
        collection.Add(14);
        collection.Add(15);
        collection.Add(16);
        collection.Add(17);
        collection.Add(18);
        collection.Add(19);
        collection.Add(20);
    }
}