using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCubesBeaviour : MonoBehaviour
{

    public Transform[] childs;
    public Vector2 min_max_height;
    public float movement_speed;
    // Start is called before the first frame update
    void Start()
    {
        childs = new Transform[transform.childCount];
       for(int i = 0; i < transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform tr in childs)
        {
            tr.position = (tr.position + (tr.up * movement_speed * Random.Range(-1, 2) * Time.deltaTime));
            if(tr.position.y < min_max_height.x)
            {
                tr.position = new Vector3(tr.position.x, min_max_height.x, tr.position.z);
            }
            if (tr.position.y > min_max_height.y)
            {
                tr.position = new Vector3(tr.position.x, min_max_height.y, tr.position.z);
            }
        }
    }
}
