using UnityEditor;
using UnityEngine;
using System.Collections;

//[CustomEditor(typeof(Card))]
public class CardEditor : Editor
{
	Card m_Instance;
	PropertyField[] m_fields;
	/*
	public void OnEnable()
	{
		m_Instance = (Card) target;
		m_fields = ExposeProperties.GetProperties(m_Instance);
	}

	public override void OnInspectorGUI()
	{
		if (m_Instance == null)
			return;
		//this.DrawDefaultInspector();
		ExposeProperties.Expose(m_fields);
	}
	*/
}