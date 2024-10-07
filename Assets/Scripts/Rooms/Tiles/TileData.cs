using UnityEngine;

///<summary>
/// TileData
///</summary>
[CreateAssetMenu(fileName = "NewTileData", menuName = "TileData")]
public class TileData : ScriptableObject{
    public string tileName;
    public Color tileColor; // this will be replaced with the tile sprite
}