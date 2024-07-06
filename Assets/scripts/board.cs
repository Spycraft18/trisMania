using UnityEngine ;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece {  get; private set; } 
    public TetrominoData[] tetrominoes;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10,20);
    public Points_Lines_Level pointsLinesLevel;

    public int LinesCleared = 0;
    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x /2, -this.boardSize.y /2);
            return new RectInt(position, this.boardSize);
        }
    }
    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>(); 
        for (int i = 0; i < this.tetrominoes.Length; i++)
        {
        this.tetrominoes[i].Initialize();

        }
    }
    private void Start()
    {
        SpawnPiece();
    }

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
        Set(this.activePiece);
    }

    public void GameOver()
    {
        if (pointsLinesLevel != null)
        {
            pointsLinesLevel.SaveScoresBeforeGameOver();
            Debug.Log("Sigamos" + pointsLinesLevel.LastScores);
        }
        this.tilemap.ClearAllTiles();
        SceneManager.LoadScene("GameOver");
        Debug.Log("A ver" + pointsLinesLevel.LastScores);
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++) 
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;
        for(int i = 0; i < piece.cells.Length;i++)
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

    public void DeleteLines()
    {
        RectInt bounds = this.Bounds;
        int rows = bounds.yMin;
        while (rows < bounds.yMax)
        {
            if (IsLineFull(rows))
            {
                LinesCleared++;
                ClearLine(rows);
            }
            else
            {
                rows++;
            }
        }
    }

    public bool IsLineFull(int row)
    {
        RectInt borde = this.Bounds;
        for (int column = borde.xMin; column < borde.xMax; column++)
        {
            Vector3Int posicion = new Vector3Int(column, row, 0);
            if (!this.tilemap.HasTile(posicion))
            {
                return false;
            }
        }
        return true;
    }

    public void ClearLine(int row)
    {
        RectInt borde = this.Bounds;

        for(int col = borde.xMin; col < borde.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row,0);
            this.tilemap.SetTile(position,null);
        }

        while(row < borde.yMax)
        {
            for (int col = borde.xMin; col < borde.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase arriba = this.tilemap.GetTile(position);
                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, arriba);
            }
            row++;
        }
    }
}
