using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gost : MonoBehaviour
{
    public Tile Tile;
    public Board Board;
    public Piece piezadeseguimiento;

    public Tilemap tilemap { get;private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }

    private void awake()
    {
        this.tilemap = GetComponent<Tilemap>(); 
        this.cells = new Vector3Int[4];

    }

    private void ultimaactualizacion()
    {
        clear();
        copy();
        drop();
        set();
    }
    private void clear()
    {
        
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3Int tilePosition = this.cells[i] + this.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    private void copy()
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            this.cells[i] = this.piezadeseguimiento.cells[i];


        }
    }
    private void drop()
    {
        Vector3Int position= this.piezadeseguimiento.position;
        int current = position.y;
        int bottom= this.Board.boardSize / 2 -1 ;
        this.Board.Clear(this.piezadeseguimiento);
        for (int row = current; row < bottom; row++)
        {
            position.y = row;
            if (this.Board.IsValidPosition(this.piezadeseguimiento, position)){
                this.position = position;

            } else {
                break;
            }
        }
        this.Board.set(this.piezadeseguimiento);

    }
    private void set()
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3Int tilePosition = this.cells[i] + this.position;
            this.tilemap.SetTile(tilePosition, this.tile);
        }
    }
}
