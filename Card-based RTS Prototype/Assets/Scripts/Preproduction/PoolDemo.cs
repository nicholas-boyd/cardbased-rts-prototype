using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PoolDemo : MonoBehaviour
{
    const string PoolKey = "Demo.Prefab";
    [SerializeField] GameObject prefab;
    List<Poolable> instances = new List<Poolable>();
    void Start()
    {
        if (GameObjectPoolController.AddEntry(PoolKey, prefab, 10, 15))
            Debug.Log("Pre-populating pool");
        else
            Debug.Log("Pool already configured");
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Scene 1"))
            ChangeLevel(0);
        if (GUI.Button(new Rect(10, 50, 100, 30), "Scene 2"))
            ChangeLevel(1);
        if (GUI.Button(new Rect(10, 90, 100, 30), "Dequeue"))
        {
            Poolable obj = GameObjectPoolController.Dequeue(PoolKey);
            float x = UnityEngine.Random.Range(-10, 10);
            float y = UnityEngine.Random.Range(0, 5);
            float z = UnityEngine.Random.Range(0, 10);
            obj.transform.localPosition = new Vector3(x, y, z);
            obj.gameObject.SetActive(true);
            instances.Add(obj);
        }
        if (GUI.Button(new Rect(10, 130, 100, 30), "Enqueue"))
        {
            if (instances.Count > 0)
            {
                Poolable obj = instances[0];
                instances.RemoveAt(0);
                GameObjectPoolController.Enqueue(obj);
            }
        }
    }
    void ChangeLevel(int level)
    {
        ReleaseInstances();
        SceneManager.LoadScene(string.Format("PoolSample{0}",level));
    }
    void ReleaseInstances()
    {
        for (int i = instances.Count - 1; i >= 0; --i)
            GameObjectPoolController.Enqueue(instances[i]);
        instances.Clear();
    }
}