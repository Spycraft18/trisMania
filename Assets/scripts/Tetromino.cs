using UnityEngine.Tilemaps;

public enum tetromino
{
    I,
    O,
    T,
    J,
    L,
    S,
    Z,
    
}
[System.Serializable] 
public struct TetrominoData
{
    public Tetromino tetromino;
    public Tile tile;
}