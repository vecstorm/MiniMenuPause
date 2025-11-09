using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotinController : MonoBehaviour
{
    Rigidbody rb;

    public float fuerza = 10f;  // magnitud del impulso
    public Vector3 direccion = Vector3.forward; // dirección del impulso

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // obtenemos el Rigidbody del objeto
    }

    void Update()
    {
        // Por ejemplo, al presionar la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DarImpulso();
        }
    }

    void DarImpulso()
    {
        // Agrega una fuerza instantánea (impulso)
        rb.AddForce(direccion.normalized * fuerza, ForceMode.Impulse);
    }
}
