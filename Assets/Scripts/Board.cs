using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tileObject;

    //PAra personalizar la posision y ortografico de la camara
    //PAra hacer Zoom en la camara
    public float cameraSizeOffset;
    //Para mover las cuadriculas segun nos convenga, arriba o abajo
    public float cameraVerticalOffset;

    // Start is called before the first frame update
    void Start()
    {
        SetupBoard();
        PositionCamera();
    }

    private void PositionCamera()
    {
        float newPosX = (float)width / 2f;
        float newPosY = (float)height / 2f;

        Camera.main.transform.position = new Vector3(newPosX-0.5f, newPosY - 0.5f + cameraVerticalOffset, -10f);

        // se obtiene el tamaño ortografico de la camara para ajustar la visual segun la cantidad de elementos creados de la matriz

        float horizontal = width + 1;
        float vertical = (height/2 ) + 1;

        Camera.main.orthographicSize = horizontal > vertical ? horizontal +cameraSizeOffset: vertical+cameraSizeOffset;

    }

    private void SetupBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cuadriculaAInstanciar = Instantiate(tileObject, new Vector3(x, y, -5), Quaternion.identity);
                cuadriculaAInstanciar.transform.parent = transform;

                //quiero obtener acceso de su componenete de tipo Tile
                cuadriculaAInstanciar.GetComponent<Tile>()?.Setup(x, y, this);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
