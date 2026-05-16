using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TileMapMouse : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Collider collider;
    TileMap tilemap;
    [SerializeField] Transform cube;

    void Start()
    {
        collider = GetComponent<Collider>();
        tilemap = GetComponent<TileMap>();
    }
    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(collider.Raycast(ray, out hit, Mathf.Infinity))
        {
            int x = Mathf.FloorToInt(hit.point.x  / tilemap.tileSize);
            int z = Mathf.FloorToInt(hit.point.z / tilemap.tileSize);
            Vector2 tile = new Vector2(x, z);
            Debug.Log(tile);
            GetComponent<Renderer>().material.color = Color.red;
            cube.position = new Vector3(tile.x, 0, tile.y) * tilemap.tileSize + new Vector3(tilemap.tileSize, 0, tilemap.tileSize) * 0.5f;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
