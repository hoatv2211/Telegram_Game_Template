using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class JsonHelper{

	public static T[] FromJson<T>(string json) {
		Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
		return wrapper.Items;
	}
	public static List<T> JsontoList<T>(string json)
	{
		Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
		List<T> temp = new List<T>();
        for (int i = 0; i < wrapper.Items.Length; i++)
        {
			temp.Add(wrapper.Items[i]);
        }


		return temp;
	}
	public static string ToJson<T>(T[] array) {
		Wrapper<T> wrapper = new Wrapper<T>();
		wrapper.Items = array;
		return UnityEngine.JsonUtility.ToJson(wrapper);
	}

    public static string ToJson<T>(List<T> list)
    {
        return ToJson<T>(list.ToArray());
    }

	[Serializable]
	private class Wrapper<T> {
		public T[] Items;
	}
}
