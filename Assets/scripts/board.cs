using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    // Tilemap que representa el tablero.
    public Tilemap tilemap { get; private set; }

    // La pieza activa actual en el tablero.
    public Piece activePiece { get; private set; }

    // Array de datos de los tetrominos (diferentes formas de piezas de Tetris).
    public TetrominoData[] tetrominoes;

    // Posici�n de generaci�n de nuevas piezas.
    public Vector3Int spawnPosition;

    // Tama�o del tablero (ancho y alto en celdas).
    public Vector2Int boardSize = new Vector2Int(10, 20);

    // Propiedad que devuelve los l�mites del tablero.
    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    // M�todo Awake, llamado al inicializar el script.
    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();
        for (int i = 0; i < this.tetrominoes.Length; i++)
        {
            this.tetrominoes[i].Initialize();
        }
    }

    // M�todo Start, llamado antes del primer frame.
    private void Start()
    {
        SpawnPiece();
    }

    // M�todo para generar una nueva pieza en el tablero.
    public void SpawnPiece()
    {
        int random = Random.Range(0, this.tetrominoes.Length);
        TetrominoData data = this.tetrominoes[random];
        this.activePiece.Initialize(this, this.spawnPosition, data);

        if (IsValidPosition(this.activePiece, this.spawnPosition))
        {
            Set(this.activePiece);
        }
        else
        {
            GameOver();
        }
    }

    // M�todo para limpiar el tablero cuando el juego termina.
    public void GameOver()
    {
        this.tilemap.ClearAllTiles();
    }

    // M�todo para colocar una pieza en el tablero.
    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    // M�todo para borrar una pieza del tablero.
    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    // M�todo para verificar si una posici�n es v�lida para una pieza.
    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;
            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }
            if (this.tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }
        return true;
    }

    // M�todo para eliminar las l�neas completas del tablero.
    public void DeleteLines()
    {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;
        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                ClearLine(row);
            }
            else
            {
                row++;
            }
        }
    }

    // M�todo para verificar si una l�nea est� completa.
    public bool IsLineFull(int row)
    {
        RectInt bounds = this.Bounds;
        for (int column = bounds.xMin; column < bounds.xMax; column++)
        {
            Vector3Int position = new Vector3Int(column, row, 0);
            if (!this.tilemap.HasTile(position))
            {
                return false;
            }
        }
        return true;
    }

    // M�todo para borrar una l�nea completa y mover las l�neas superiores hacia abajo.
    public void ClearLine(int row)
    {
        RectInt bounds = this.Bounds;

        // Borra todas las celdas en la l�nea completa.
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(position, null);
        }

        // Mueve todas las l�neas superiores hacia abajo.
        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = this.tilemap.GetTile(position);
                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, above);
            }
            row++;
        }
    }
}

