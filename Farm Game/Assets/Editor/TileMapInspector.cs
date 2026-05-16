using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMap))]
public class TileMapInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TileMap tileMap = (TileMap)target;
        if (GUILayout.Button("Regenerate"))
        {
            tileMap.BuildMesh(tileMap.sizeX, tileMap.sizeZ, tileMap.tileSize);
        }
    }
}
