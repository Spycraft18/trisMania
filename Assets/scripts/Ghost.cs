using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
    // La variable 'tile' representa el Tile que se usará para dibujar la pieza fantasma.
    public Tile tile;

    // Referencia al tablero principal del juego.
    public Board mainBoard;

    // Referencia a la pieza actual que está siendo seguida por la pieza fantasma.
    public Piece trackingPiece;

    // La variable 'tilemap' se usa para manejar el Tilemap en el que se dibujará la pieza fantasma.
    public Tilemap tilemap { get; private set; }

    // Un arreglo que contiene las celdas de la pieza fantasma.
    public Vector3Int[] cells { get; private set; }

    // La posición de la pieza fantasma.
    public Vector3Int position { get; private set; }

    // Se inicializan las variables en el método Awake, que se llama cuando el script se inicializa.
    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        cells = new Vector3Int[4];
    }

    // El método LateUpdate se llama una vez por cada frame, después de que todos los Update hayan sido llamados.
    private void LateUpdate()
    {
        Clear(); // Borra la pieza fantasma del Tilemap.
        Copy();  // Copia las celdas de la pieza actual.
        Drop();  // Calcula la posición más baja posible para la pieza fantasma.
        Set();   // Dibuja la pieza fantasma en el Tilemap.
    }

    // Borra la pieza fantasma del Tilemap.
    private void Clear()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = this.cells[i] + this.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    // Copia las celdas de la pieza actual.
    private void Copy()
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            this.cells[i] = trackingPiece.cells[i];
        }
    }

    // Calcula la posición más baja posible para la pieza fantasma.
    private void Drop()
    {
        Vector3Int position = this.trackingPiece.position;

        // La posición Y más baja posible.
        int current = position.y;
        int bottom = -this.mainBoard.boardSize.y / 2 - 1;

        // Limpia la pieza actual del tablero para evitar colisiones consigo misma.
        this.mainBoard.Clear(trackingPiece);

        // Mueve la pieza fantasma hacia abajo hasta que no sea una posición válida.
        for (int row = current; row >= bottom; row--)
        {
            position.y = row;

            if (this.mainBoard.IsValidPosition(this.trackingPiece, position))
            {
                this.position = position;
            }
            else
            {
                break;
            }
        }

        // Vuelve a poner la pieza actual en el tablero.
        this.mainBoard.Set(this.trackingPiece);
    }

    // Dibuja la pieza fantasma en el Tilemap.
    private void Set()
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3Int tilePosition = this.cells[i] + this.position;
            this.tilemap.SetTile(tilePosition, this.tile);
        }
    }
}