using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;

public class Piece : MonoBehaviour
{
    public Board board { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public TetrominoData data { get; private set; }
    public Vector3Int position { get; private set; }
    public Points_Lines_Level Points_Lines_Level;
    public Pause pause;

    public int rotationIndex { get; private set; }
    public float stepdelay = 1f;
    public float lockdelay = 0.5f;

    private float steptime;
    private float locktime;


    public void Initialize(Board board, Vector3Int position, TetrominoData data)
    {
        this.rotationIndex = 0;
        this.board = board;
        this.position = position;
        this.data = data;
        this.steptime = Time.time + this.stepdelay;
        this.locktime = 0f;
        if (this.cells == null)
        {
            this.cells = new Vector3Int[data.cells.Length];
        }
        for (int i = 0; i < data.cells.Length; i++)
        {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }
    private void Start()
    {
        // Buscar el objeto Pause en la escena
        pause = FindObjectOfType<Pause>();
        Points_Lines_Level = FindObjectOfType<Points_Lines_Level>();
    }

    private void Update()
    {

        this.board.Clear(this);
        this.locktime += Time.deltaTime;
        if (pause != null && pause.movement)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(Vector2Int.left);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Move(Vector2Int.right);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Move(Vector2Int.down);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Rotate(-1);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Rotate(1);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HardDrop();
            }
            if (Time.time >= this.steptime)
            {
                Step();
            }
        }
        this.board.Set(this);

    }
    private void Step()
    {
        this.steptime = Time.time + this.stepdelay;

        Move(Vector2Int.down);

        if (this.locktime >= this.lockdelay)
        {
            Lock();

        }
    }
    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }
        Lock();
    }
    private void Lock()
    {
        this.board.Set(this);
        this.board.DeleteLines();
        this.board.SpawnPiece();
    }


    private bool Move(Vector2Int translation)
    {
        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;
        bool valid = this.board.IsValidPosition(this, newPosition);

        if (valid)
        {
            this.position = newPosition;
            this.locktime = 0f;
        }
        return valid;
    }

    private void Rotate(int direction)
    {
        int oldRotation = this.rotationIndex;
        this.rotationIndex = Wrap(this.rotationIndex + direction, 0, 4);
        MatrixRotation(direction);

        if (!TestWallKicks(this.rotationIndex, direction))
        {
            this.rotationIndex = oldRotation;
            MatrixRotation(-direction);
        }
    }

    private void MatrixRotation(int direction)
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3 cell = this.cells[i];
            int x, y;

            switch (this.data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
            }
            this.cells[i] = new Vector3Int(x, y, 0);
        }
    }
    private bool TestWallKicks(int RotationIndex, int RotationDirection)
    {
        int WallKickIndex = GetWallKicks(RotationIndex, RotationDirection);
        for (int i = 0; i < this.data.wallkicks.GetLength(1); i++)
        {
            Vector2Int traslacion = this.data.wallkicks[WallKickIndex, i];

            if (Move(traslacion))
            {
                return true;
            }
        }
        return false;
    }

    private int GetWallKicks(int RotationIndex, int RotationDirection)
    {
        int wallkickIndex = RotationIndex * 2;

        if (RotationDirection < 0)
        {
            wallkickIndex--;
        }
        return Wrap(wallkickIndex, 0, this.data.wallkicks.GetLength(0));
    }
    private int Wrap(int input, int min, int max)
    {
        if (input < min)
        {
            return max - (min - input) % (max - min);
        }
        else
        {
            return min + (input - min) % (max - min);
        }
    }
}

