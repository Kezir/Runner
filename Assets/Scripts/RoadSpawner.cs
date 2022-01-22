using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads_ref = new List<GameObject>();
    public Queue<GameObject> roads = new Queue<GameObject>();
    public int length = 10;

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
    }


    private void CheckPlayer()
    {
        if (PlayerMovement.Instance.transform.position.z - roads.Peek().transform.position.z > 20)
        {
            roads.Peek().transform.position += new Vector3(0, 0, 10 * roads_ref.Count);
            roads.Enqueue(roads.Peek());
            roads.Dequeue();
        }
    }

    public void Init()
    {
        StartRoad();
        EnqueueList();
    }

    private void EnqueueList()
    {
        foreach(var obj in roads_ref)
        {
            roads.Enqueue(obj);
        }
    }

    private void StartRoad()
    {
        for(int i = 0; i<roads_ref.Count;i++)
        {
            roads_ref[i].transform.position = new Vector3(0, 0, i * 10);
        }
    }
}
