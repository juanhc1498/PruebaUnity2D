using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //Variables
    public GameObject Player;
    private Vector3 CameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        //Obtengo la posicion del jugador
        CameraPosition = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Transformo la posicion de la camara a la del jugador
        transform.position = Player.transform.position + CameraPosition;
    }
}
