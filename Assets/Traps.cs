using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{

    public float amplitude = 0.5f; // Amplitud del movimiento
    public float speed = 1.0f; // Velocidad del movimiento
    private float startY; // Posición inicial del objeto

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = startY + amplitude * Mathf.Cos(speed * Time.time); // Calcula la nueva posición Y del objeto
        transform.position = new Vector3(transform.position.x, newY, transform.position.z); // Actualiza la posición del objeto
    }
}
