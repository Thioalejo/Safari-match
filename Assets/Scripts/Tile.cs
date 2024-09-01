using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    // con esto podemos llevar cuenta de que espacio representa cada tile que tengamos
    public int x;
    public int y;
    public Board board;

    public void Setup(int x_, int y_, Board board_)
    {
        x = x_;
        y = y_;
        board = board_;
    }

    //Se agrega el input del mouse para detectar los clics

    public void OnMouseDown()
    {
        board.TileDown(this);
    }

    public void OnMouseEnter()
    {
        board.TileOver(this);
    }

    private void OnMouseUp()
    {
        board.TileUp(this);
    }
}
