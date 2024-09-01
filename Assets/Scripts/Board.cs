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

    public GameObject[] availablePieces;

    //Para el intercambio de piezas
    Tile[,] Tiles;
    Piece[,] Pieces;

    Tile startTile;
    Tile endTile;


    // Start is called before the first frame update
    void Start()
    {
        Tiles = new Tile[width, height];
        Pieces = new Piece[width, height];
        SetupBoard();
        PositionCamera();
        SetupPieces();
    }

    private void SetupPieces()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var selectedPiece = availablePieces[UnityEngine.Random.Range(0, availablePieces.Length)];
                var cuadriculaAInstanciar = Instantiate(selectedPiece, new Vector3(x, y, -5), Quaternion.identity);
                cuadriculaAInstanciar.transform.parent = transform;

                //quiero obtener acceso de su componenete de tipo Tile y PAra guardar la referencia de las piezas que estamos creando
                Pieces[x,y] = cuadriculaAInstanciar.GetComponent<Piece>();
                Pieces[x, y].Setup(x, y, this);
            }
        }
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

                //PAra guardar la referencia de los espacios que estamos creando
                Tiles[x, y] = cuadriculaAInstanciar.GetComponent<Tile>();
                Tiles[x, y]?.Setup(x, y, this);

                //quiero obtener acceso de su componenete de tipo Tile
                cuadriculaAInstanciar.GetComponent<Tile>()?.Setup(x, y, this);
            }
        }
    }

    // recibira el espacio o pieza que fue clickeado
    public void TileDown(Tile  tile_)
    {
        startTile = tile_;
    }

    //PAra cuando arrastro el maouse encima de otra cuadricula
    public void TileOver(Tile tile_)
    {
        endTile = tile_;
    }
    
    //Cuando levanto el clic
    public void TileUp(Tile tile_)
    {
        if(startTile!=null && endTile != null && IsCloseTo(startTile, endTile))
        {
            SwapTiles();
        }
        startTile = null;
        endTile = null;
    }

    //Para actualizar el sistema de coordenadas de los arrays de 2 dimensiones creados arriba
    private void SwapTiles()
    {
        //Obtenemos la referencia de las pizas (posicion)
        var StartPiece = Pieces[startTile.x, startTile.y];
        var EndPiece = Pieces[endTile.x, endTile.y];

        //Ahora las movemos
        StartPiece.Move(endTile.x, endTile.y);
        EndPiece.Move(startTile.x, startTile.y);

        //Actualizamos el sistema de coordenadas de Pieces para que quede como se movieron
        Pieces[startTile.x, startTile.y] = EndPiece;
        Pieces[endTile.x, endTile.y] = StartPiece;

    }

    public bool IsCloseTo(Tile start, Tile end)
    {
        //Validamos movimiento horizontal
        if(Math.Abs((start.x - end.x))==1 && start.y == end.y){
            return true;
        }

        if (Math.Abs((start.y - end.y)) == 1 && start.x == end.x)
        {
            return true;
        }

        return false;
    }
}
