using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWorld : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objects = new GameObject[10];

    // 0 = top, 1 = Cone Out, 2 = Cone In, 3 = Slope, 4 = Lone
    // Start is called before the first frame update
    void Start()
    {
        //for (int x = -80; x <= 80; x = x + 2)
        //{
        //    for (int z = 0; z <= 160; z = z + 2)
        //    {
        //        if(Random.Range(0,10) > 6) {
        //           GameObject obj = GameObject.Instantiate(objects[0], new Vector3(x, 2, z), Quaternion.identity, this.transform);
        //           // obj.transform.localScale = new Vector3(5, 10, 5);
        //        }
        //    }
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
