using UnityEngine;

[System.Serializable]
public class Card {

	[HideInInspector, SerializeField]
	private CardType.ValueEnum value;

	[ExposeProperty]
	public CardType.ValueEnum Value
	{
		get { return this.value; }
		set { 
			this.value = value;
		}
	}

	[HideInInspector, SerializeField]
	private CardType.SuitEnum suit;

	[ExposeProperty]
	public CardType.SuitEnum Suit
	{
		get { return suit; }
		set { 
			suit = value;
			color = CardType.ColorMap[suit];
		}
	}
		
	[HideInInspector, SerializeField]
	private CardType.ColorEnum color;
	public CardType.ColorEnum Color { 
		get { return color; }
	}
}