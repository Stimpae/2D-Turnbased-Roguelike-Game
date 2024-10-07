using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace DefaultNamespace {
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class RoomCollectionData : ScriptableObject {
        [SerializeField] private List<RoomData> roomData = new List<RoomData>();
        
        public RoomData GetRoomData(ERoomType roomType) {
            var list = roomData.FindAll(data => data.roomType == roomType);
            return list[Random.Range(0, list.Count)];
        }
    }
}