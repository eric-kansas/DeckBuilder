using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;

[CustomPropertyDrawer (typeof (Card))]
public class CardDrawer : PropertyDrawer {

	const int labelWidth = 45;
	const int suitWidth = 80;
	const int label2Width = 60;

	public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {
		//List<Card> m_Instance = (List<Card>) fieldInfo.GetValue(prop);
		//Debug.Log(m_Instance);
		//PropertyField[] m_fields = ExposeProperties.GetProperties(fieldInfo.GetValue(prop));

		//Debug.Log(fieldInfo.GetValue(prop.serializedObject.targetObject));
		SerializedProperty value = prop.FindPropertyRelative ("value");
		SerializedProperty suit = prop.FindPropertyRelative ("suit");
		SerializedProperty color = prop.FindPropertyRelative ("color");

		//Debug.Log(Util.GetEnumValue<CardType.ValueEnum>(value));

		EditorGUI.LabelField(new Rect(pos.x, pos.y, labelWidth, pos.height), "Card: ");

		pos.x += labelWidth;

		PropertyField[] test = ExposeProperties.GetProperties(Util.GetTargetObjectOfProperty(prop));

		foreach (PropertyField field in test) {
			ExposeProperties.Expose(field, new Rect (pos.x, pos.y, suitWidth, pos.height));
			pos.x += suitWidth;
		}

		/*EditorGUI.PropertyField (
			new Rect (pos.x, pos.y, suitWidth, pos.height),
			color, GUIContent.none);*/
		EditorGUI.LabelField(new Rect(pos.x, pos.y, label2Width, pos.height), "" + Util.GetEnumValue<CardType.ColorEnum>(color));

	}
}
