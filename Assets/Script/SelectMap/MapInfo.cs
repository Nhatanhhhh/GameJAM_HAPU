using UnityEngine;

[CreateAssetMenu(fileName = "NewMapInfo", menuName = "Game Data/Map Info")]
public class MapInfo : ScriptableObject
{
    public string mapName;
    public string sceneName;       // Tên scene cần load
    public Sprite thumbnail;       // Hình ảnh preview
}
