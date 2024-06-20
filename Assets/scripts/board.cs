using UnityEngine ;
using UnityEngine.Tilemaps;
public class board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public TetrominoData[] tetrominoes;
    private void awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
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
    }

    public void Set()
    {

    }
}
