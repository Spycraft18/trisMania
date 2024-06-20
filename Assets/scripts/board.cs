using UnityEngine ;
using UnityEngine.Tilemaps;
public class board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public TetrominoData[] tetrominoes;
    private void awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        for (int i = 0; i < this.tetrominoes.Length; i++) ;
        this.tetrominoes[i].Initialize();
    }
}
