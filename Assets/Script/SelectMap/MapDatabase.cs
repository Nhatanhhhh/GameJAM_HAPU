using UnityEngine;

[CreateAssetMenu(fileName = "MapDatabase", menuName = "Game Data/Map Database")]
public class MapDatabase : ScriptableObject
{
    public MapInfo[] maps; // Mảng chứa tất cả các map
}
