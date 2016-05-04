using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardType {
	public enum ColorEnum {
		None=0,
		Red=1,
		Black=2,
		Any=3
	}

	public static Dictionary<SuitEnum, ColorEnum> ColorMap = new Dictionary<SuitEnum, ColorEnum>()	{
		{ SuitEnum.None, ColorEnum.None },
		{ SuitEnum.Hearts, ColorEnum.Red },
		{ SuitEnum.Clubs, ColorEnum.Black },
		{ SuitEnum.Diamonds, ColorEnum.Red },
		{ SuitEnum.Spades, ColorEnum.Black },
		{ SuitEnum.Any, ColorEnum.Any },
	};

	public enum SuitEnum {
		None=0,
		Hearts=1,
		Clubs=2,
		Diamonds=3,
		Spades=4,
		Any=5
	}

	public enum ValueEnum {
		None=0,
		Ace=1,
		Two=2,
		Three=3,
		Four=4,
		Five=5,
		Six=6,
		Seven=7,
		Eight=8,
		Nine=9,
		Ten=10,
		Jack=11,
		Queen=12,
		King=13,
		Any=14
	}
}
