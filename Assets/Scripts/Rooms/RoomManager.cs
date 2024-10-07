using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TG.Attributes;
using TG.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

public class RoomManager : Singleton<RoomManager> {
    
    [SerializeField, ScriptableObject] RoomCollectionData roomCollectionData;
    [SerializeField] GameObject tilePrefab;
    
    private RoomGenerator m_roomGenerator;
    
    private void Start() {
        m_roomGenerator = new RoomGenerator(tilePrefab);
        GenerateRoom(ERoomType.FOREST);
    }

    public void GenerateRoom(ERoomType roomType) {
        m_roomGenerator.GenerateRoom(roomCollectionData.GetRoomData(roomType));
    }
    
    public Tile GetTile(Vector2 position) {
        return m_roomGenerator.GetTile(position);
    }
}