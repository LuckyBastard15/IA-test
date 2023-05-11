using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public int speed = 4;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0f, 0f, 45f * Time.deltaTime * speed);
    }
}
