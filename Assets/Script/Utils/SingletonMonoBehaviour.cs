using System;
using UnityEngine;

public abstract class Singleton<T> where T : new()
{
	private static T singleton;
	public static T Instance
	{
		get
		{
			if (singleton == null)
			{
				singleton = new T();
			}
			return singleton;
		}
	}
	public static T instance
	{
		get
		{
			if (singleton == null)
			{
				singleton = new T();
			}
			return singleton;
		}
	}
}
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T singleton;

	public static bool IsInstanceValid() { return singleton != null; }

	void Reset()
	{
		gameObject.name = typeof(T).Name;
	}

	public static T Instance
	{
		get
		{
			if (!Application.isPlaying)
			{
				return null;
			}
			if (SingletonMono<T>.singleton == null)
			{
				SingletonMono<T>.singleton = (T)FindObjectOfType(typeof(T));
				if (SingletonMono<T>.singleton == null)
				{
					GameObject obj = new GameObject();
					obj.name = "[@" + typeof(T).Name + "]";
					SingletonMono<T>.singleton = obj.AddComponent<T>();
				}
			}

			return SingletonMono<T>.singleton;
		}
	}
	public static T instance
	{
		get
		{
			if (!Application.isPlaying)
			{
				return null;
			}
			if (SingletonMono<T>.singleton == null)
			{
				SingletonMono<T>.singleton = (T)FindObjectOfType(typeof(T));
				if (SingletonMono<T>.singleton == null)
				{
					GameObject obj = new GameObject();
					obj.name = "[@" + typeof(T).Name + "]";
					SingletonMono<T>.singleton = obj.AddComponent<T>();
				}
			}

			return SingletonMono<T>.singleton;
		}
	}

	public static T GetInstance(bool autoNew = false)
	{
		if (!Application.isPlaying)
		{
			return null;
		}

		if (!autoNew) return SingletonMono<T>.singleton;

		SingletonMono<T>.singleton = (T)FindObjectOfType(typeof(T));
		if (SingletonMono<T>.singleton == null)
		{
			GameObject obj = new GameObject();
			obj.name = "[@" + typeof(T).Name + "]";
			SingletonMono<T>.singleton = obj.AddComponent<T>();
		}

		return SingletonMono<T>.singleton;
	}
}


public class SingletonMonoDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T singleton;

	public static bool IsInstanceValid() { return singleton != null; }

	void Reset()
	{
		gameObject.name = typeof(T).Name;
	}


	public static T Instance
	{
		get
		{
			if (!Application.isPlaying)
			{
				return null;
			}
			if (SingletonMonoDontDestroy<T>.singleton == null)
			{
				SingletonMonoDontDestroy<T>.singleton = (T)FindObjectOfType(typeof(T));
				if (SingletonMonoDontDestroy<T>.singleton == null)
				{
					GameObject obj = new GameObject();
					obj.name = "[@" + typeof(T).Name + "]";
					SingletonMonoDontDestroy<T>.singleton = obj.AddComponent<T>();
					DontDestroyOnLoad(obj);
				}
			}

			return SingletonMonoDontDestroy<T>.singleton;
		}
	}
}

