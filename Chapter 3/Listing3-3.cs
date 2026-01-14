using Mirror;
using UnityEngine;

public static class CustomNetworkSerializers
{
    public abstract class CardKeyword : ScriptableObject
    {
        public uint ID;
        //many more fields here which can not be serialized automatically
    }

    public interface ICardKeywordSource
    {
        short GetSourceID(); //gets the ID of the Card that assigned the Keyword to the card being serialized
    }

    public static void WriteSerializedDictionary(NetworkWriter writer, Dictionary<CardKeyword, ICardKeywordSource> dictionary)
    {
        writer.WriteInt(dictionary.Count);
        foreach (var item in dictionary)
        {
            writer.WriteUInt(item.Key.ID);
            writer.WriteShort(item.Value.GetSourceID());
        }
    }

    public static void ReadSerializedDictionary(NetworkReader reader, out Dictionary<CardKeyword, ICardKeywordSource> dictionary, PlayerManager cardOwner)
    {
        int dictionaryLenght = reader.ReadInt();
        dictionary = new Dictionary<CardKeyword, ICardKeywordSource>();
        if (dictionaryLenght < 1)
        { 
            return; 
        }
        for (int i = 0; i < dictionaryLenght; i++)
        {
            uint keywordID = reader.ReadUInt();
            short sourceID = reader.ReadShort();
            ICardKeywordSource source = null;

            foreach (var card in cardOwner.allMyCards)
            {
                if (card.ID != sourceID) { continue; }
                source = card;
                break;
            }
            dictionary[KeywordsDatabase.GetByID(keywordID)] = source;
            if (source == null)
            {
                Debug.LogError($"Couldn't recognize KeywordSource with ID {sourceID} on client");
                continue;
            }
        }
    }
}