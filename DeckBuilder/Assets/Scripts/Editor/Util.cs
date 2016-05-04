using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class Util {

	public static object GetTargetObjectOfProperty(SerializedProperty prop)
	{
		var path = prop.propertyPath.Replace(".Array.data[", "[");
		object obj = prop.serializedObject.targetObject;
		var elements = path.Split('.');
		foreach (var element in elements)
		{
			if (element.Contains("["))
			{
				var elementName = element.Substring(0, element.IndexOf("["));
				var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
				obj = GetValue_Imp(obj, elementName, index);
			}
			else
			{
				obj = GetValue_Imp(obj, element);
			}
		}
		return obj;
	}

	private static object GetValue_Imp(object source, string name)
	{
		if (source == null)
			return null;
		var type = source.GetType();

		while (type != null)
		{
			var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			if (f != null)
				return f.GetValue(source);

			var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
			if (p != null)
				return p.GetValue(source, null);

			type = type.BaseType;
		}
		return null;
	}

	private static object GetValue_Imp(object source, string name, int index)
	{
		var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
		if (enumerable == null) return null;
		var enm = enumerable.GetEnumerator();
		//while (index-- >= 0)
		//    enm.MoveNext();
		//return enm.Current;

		for (int i = 0; i <= index; i++)
		{
			if (!enm.MoveNext()) return null;
		}
		return enm.Current;
	}

	public static object GetPropertyValue(SerializedProperty prop)
	{
		if (prop == null) throw new System.ArgumentNullException("prop");

		switch (prop.propertyType)
		{
		case SerializedPropertyType.Integer:
			return prop.intValue;
		case SerializedPropertyType.Boolean:
			return prop.boolValue;
		case SerializedPropertyType.Float:
			return prop.floatValue;
		case SerializedPropertyType.String:
			return prop.stringValue;
		case SerializedPropertyType.Color:
			return prop.colorValue;
		case SerializedPropertyType.ObjectReference:
			return prop.objectReferenceValue;
		case SerializedPropertyType.LayerMask:
			return (LayerMask)prop.intValue;
		case SerializedPropertyType.Enum:
			return prop.enumValueIndex;
		case SerializedPropertyType.Vector2:
			return prop.vector2Value;
		case SerializedPropertyType.Vector3:
			return prop.vector3Value;
		case SerializedPropertyType.Vector4:
			return prop.vector4Value;
		case SerializedPropertyType.Rect:
			return prop.rectValue;
		case SerializedPropertyType.ArraySize:
			return prop.arraySize;
		case SerializedPropertyType.Character:
			return (char)prop.intValue;
		case SerializedPropertyType.AnimationCurve:
			return prop.animationCurveValue;
		case SerializedPropertyType.Bounds:
			return prop.boundsValue;
		case SerializedPropertyType.Gradient:
			throw new System.InvalidOperationException("Can not handle Gradient types.");
		}

		return null;
	}

	public static SerializedPropertyType GetPropertyType(System.Type tp)
	{
		if (tp == null) throw new System.ArgumentNullException("tp");

		if(tp.IsEnum) return SerializedPropertyType.Enum;

		var code = System.Type.GetTypeCode(tp);
		switch(code)
		{
		case System.TypeCode.SByte:
		case System.TypeCode.Byte:
		case System.TypeCode.Int16:
		case System.TypeCode.UInt16:
		case System.TypeCode.Int32:
			return SerializedPropertyType.Integer;
		case System.TypeCode.Boolean:
			return SerializedPropertyType.Boolean;
		case System.TypeCode.Single:
			return SerializedPropertyType.Float;
		case System.TypeCode.String:
			return SerializedPropertyType.String;
		case System.TypeCode.Char:
			return SerializedPropertyType.Character;
		default:
			{
				if (IsType(tp, typeof(Color)))
					return SerializedPropertyType.Color;
				else if (IsType(tp, typeof(UnityEngine.Object)))
					return SerializedPropertyType.ObjectReference;
				else if (IsType(tp, typeof(LayerMask)))
					return SerializedPropertyType.LayerMask;
				else if (IsType(tp, typeof(Vector2)))
					return SerializedPropertyType.Vector2;
				else if (IsType(tp, typeof(Vector3)))
					return SerializedPropertyType.Vector3;
				else if (IsType(tp, typeof(Vector4)))
					return SerializedPropertyType.Vector4;
				else if (IsType(tp, typeof(Quaternion)))
					return SerializedPropertyType.Quaternion;
				else if (IsType(tp, typeof(Rect)))
					return SerializedPropertyType.Rect;
				else if (IsType(tp, typeof(AnimationCurve)))
					return SerializedPropertyType.AnimationCurve;
				else if (IsType(tp, typeof(Bounds)))
					return SerializedPropertyType.Bounds;
				else if (IsType(tp, typeof(Gradient)))
					return SerializedPropertyType.Gradient;
			}
			return SerializedPropertyType.Generic;

		}
	}

	public static T GetEnumValue<T>(this SerializedProperty prop) where T : struct, System.IConvertible
	{
		if (prop == null) throw new System.ArgumentNullException("prop");

		try
		{
			var name = prop.enumNames[prop.enumValueIndex];
			return ToEnum<T>(name);
		}
		catch
		{
			return default(T);
		}
	}

	public static T ToEnum<T>(string val, T defaultValue) where T : struct, System.IConvertible
	{
		if (!typeof(T).IsEnum) throw new System.ArgumentException("T must be an enumerated type");

		try
		{
			T result = (T)System.Enum.Parse(typeof(T), val, true);
			return result;
		}
		catch
		{
			return defaultValue;
		}
	}

	public static T ToEnum<T>(int val, T defaultValue) where T : struct, System.IConvertible
	{
		if (!typeof(T).IsEnum) throw new System.ArgumentException("T must be an enumerated type");

		object obj = val;
		if(System.Enum.IsDefined(typeof(T), obj))
		{
			return (T)obj;
		}
		else
		{
			return defaultValue;
		}
	}

	public static T ToEnum<T>(object val, T defaultValue) where T : struct, System.IConvertible
	{
		return ToEnum<T>(System.Convert.ToString(val), defaultValue);
	}

	public static T ToEnum<T>(string val) where T : struct, System.IConvertible
	{
		return ToEnum<T>(val, default(T));
	}

	public static T ToEnum<T>(int val) where T : struct, System.IConvertible
	{
		return ToEnum<T>(val, default(T));
	}

	public static T ToEnum<T>(object val) where T : struct, System.IConvertible
	{
		return ToEnum<T>(System.Convert.ToString(val), default(T));
	}

	public static bool IsType(System.Type tp, System.Type assignableType)
	{
		return assignableType.IsAssignableFrom(tp);
	}

	public static bool IsType(System.Type tp, params System.Type[] assignableTypes)
	{
		foreach (var otp in assignableTypes)
		{
			if (otp.IsAssignableFrom(tp)) return true;
		}

		return false;
	}


	public static int GetChildPropertyCount(SerializedProperty property, bool includeGrandChildren = false)
	{
		var pstart = property.Copy();
		var pend = property.GetEndProperty();
		int cnt = 0;

		pstart.Next(true);
		while(!SerializedProperty.EqualContents(pstart, pend))
		{
			cnt++;
			pstart.Next(includeGrandChildren);
		}

		return cnt;
	}
}
