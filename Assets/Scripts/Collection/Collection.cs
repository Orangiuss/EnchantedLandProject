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
    }
}