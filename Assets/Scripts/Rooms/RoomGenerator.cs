using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RoomGenerator {
    private readonly Dictionary<Vector2, Tile> m_tiles = new Dictionary<Vector2, Tile>();
    private readonly GameObject m_tilePrefab;
    
    public Tile GetTile(Vector2 position) {
        return m_tiles[position];
    }
    
    public RoomGenerator(GameObject tilePrefab) {
        m_tiles.Clear();
        m_tilePrefab = tilePrefab;
    }
    
    public void GenerateRoom(RoomData roomData) {
        var parent = new GameObject("Room" + roomData.roomType);
        for (var x = 0; x < roomData.roomWidth; x++) {
            for (var y = 0; y < roomData.roomHeight; y++) {
                foreach (var tiles in roomData.tileSettings) {
                    if (Random.Range(0, 100) > tiles.chanceToSpawn) continue;
                    var tileData = tiles.tileData;
                    var tile = Object.Instantiate(m_tilePrefab, new Vector3(x, y, 0), Quaternion.identity, parent.transform);
                    
                    var tileComponent = tile.GetComponent<Tile>();
                    tileComponent.Initialise(tileData);
                    
                    m_tiles.Add(new Vector2(x, y), tile.GetComponent<Tile>());
                    tile.name = $"Tile_{x}_{y}";
                    break;
                    
                }
            }
        }
    }
}
