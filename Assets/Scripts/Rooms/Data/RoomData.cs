using System;
using UnityEngine;

///<summary>
/// RoomData
///</summary>
[CreateAssetMenu(fileName = "NewRoomData", menuName = "RoomData")]
public class RoomData : ScriptableObject{
    public ERoomType roomType;
    
    // Generation settings
    public int roomWidth;
    public int roomHeight;
    
    // Tile settings
    public TileSettings[] tileSettings;

}
