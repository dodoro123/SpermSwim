using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EntityPool : Core.MonoSingleton<EntityPool> {
	
	// Use this for initialization
	
	private Dictionary<string,Object> pool;
	private Dictionary<string,List<GameObject>> inuse;
	private Dictionary<string,List<GameObject>> unuse;
	public void Initialize()
	{

		pool = new Dictionary<string, Object> ();
		pool.Add("Sperm",Resources.Load("Prefabs/Sperm"));
		
        inuse = new Dictionary<string,List<GameObject>> ();
		unuse = new Dictionary<string,List<GameObject>> ();
	}
    public void SetupSingleton()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
    }

    public T Use<T>() where T:Component
    {
        GameObject obj = Use(typeof(T).ToString());
        return obj.GetComponent<T>();
    }

	public GameObject Use(string type)
	{
		//Debug.Log ("Use Entity type:" + type);
		GameObject entity;
		if (pool[type] != null) {
			
			if(unuse.ContainsKey(type)&&unuse[type].Count!=0)
			{
				entity = unuse[type][0];
				unuse[type].RemoveAt(0);
			}
			else
			{
				entity = Instantiate(pool[type]) as GameObject;
				entity.SendMessage("Init",SendMessageOptions.DontRequireReceiver);

			}

			if(!inuse.ContainsKey(type))
			{
				inuse[type]= new List<GameObject>();
			}
			if(!inuse[type].Contains(entity))inuse[type].Add(entity);

			entity.SetActive(true);
			entity.SendMessage("Reset",SendMessageOptions.DontRequireReceiver);
			return  entity;
		}
		return null;
	}
	
	public void Reclaim(GameObject obj, string type)
	{
		if (inuse.ContainsKey(type)) {
			inuse [type].Remove(obj);
			if(!unuse.ContainsKey(type))
			{
				unuse[type]= new List<GameObject>();
			}
			if(!unuse[type].Contains(obj))unuse[type].Add(obj);
			obj.SendMessage("Dead",SendMessageOptions.DontRequireReceiver);
			obj.SetActive(false);
		}
	
	}
	
	public void ReleasePool()
	{
		
		foreach (KeyValuePair<string,List<GameObject>> list in inuse) {
			for(int i = 0;i<list.Value.Count;i++)
			{
				Destroy(list.Value[i]);
			}
		}
		inuse = new Dictionary<string, List<GameObject>> ();
		foreach (KeyValuePair<string,List<GameObject>> list in unuse) {
			for(int i = 0;i<list.Value.Count;i++)
			{
				Destroy(list.Value[i]);
			}
		}
		unuse = new Dictionary<string, List<GameObject>> ();
	}
	
	protected override void Awake () {
        base.Awake();
		Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
