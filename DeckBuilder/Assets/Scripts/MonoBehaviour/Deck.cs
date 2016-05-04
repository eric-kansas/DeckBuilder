using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Deck : MonoBehaviour {

	public List<Card> _deck = new List<Card>();

	private Dictionary<string, int> dictionary = new Dictionary<string, int>();

	public Card DrawCard() {
		return null;
	}

	public void Count() {
		dictionary.Clear();
		foreach (Card card in _deck) {
			incrementInDictionary(Enum.GetName(typeof(CardType.SuitEnum), card.Suit));
			incrementInDictionary(Enum.GetName(typeof(CardType.ValueEnum), card.Value));
			incrementInDictionary(Enum.GetName(typeof(CardType.ColorEnum), card.Color));
		}

		foreach(CardType.ValueEnum cardValue in Enum.GetValues(typeof(CardType.ValueEnum))){
			string value = Enum.GetName(typeof(CardType.ValueEnum), cardValue);
			if (dictionary.ContainsKey(value)) {
				Debug.Log(value + ": " + dictionary[value]);
			} else {
				Debug.Log(value + ": " + 0);
			}
		}

		foreach(CardType.SuitEnum cardSuit in Enum.GetValues(typeof(CardType.SuitEnum))){
			string suitName = Enum.GetName(typeof(CardType.SuitEnum), cardSuit);
			if (dictionary.ContainsKey(suitName)) {
				Debug.Log(suitName + ": " + dictionary[suitName]);
			} else {
				Debug.Log(suitName + ": " + 0);
			}
		}

		foreach(CardType.ColorEnum cardColor in Enum.GetValues(typeof(CardType.ColorEnum))){
			string color = Enum.GetName(typeof(CardType.ColorEnum), cardColor);
			if (dictionary.ContainsKey(color)) {
				Debug.Log(color + ": " + dictionary[color]);
			} else {
				Debug.Log(color + ": " + 0);
			}
		}
	}

	private void incrementInDictionary(string key) {
		int value = 0;
		dictionary.TryGetValue(key, out value);
		dictionary[key] = value + 1;
	}
}
