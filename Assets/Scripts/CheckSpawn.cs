using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpawn : MonoBehaviour
{
    public float distance = 20;
    private void Start()
    {
        InvokeRepeating("CheckPlayer",2,2);
    }

    private void CheckPlayer()
    {
        if(PlayerMovement.Instance.transform.position.z - gameObject.transform.position.z > 20)
        {
            gameObject.transform.position += new Vector3(0,0,100);
        }
    }
}
