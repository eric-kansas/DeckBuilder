using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;

[CustomPropertyDrawer (typeof (CardType))]
public class CardTypeDrawer : PropertyDrawer {

	const int labelWidth = 45;
	const int suitWidth = 80;

	public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {
	

	}
}