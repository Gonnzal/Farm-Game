using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer)), RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour
{
    public int sizeX;
    public int sizeZ;
    public float tileSize;
    public TDMap map;
    public Material groundMat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        map = new TDMap(sizeX, sizeZ);
        BuildMesh(sizeX, sizeZ, tileSize);
    }


    public void BuildMesh(int sizeX, int sizeZ, float tileSize)
    {
        int numTiles = sizeX * sizeZ;
        int numTris = numTiles * 2;
        
        int vSizeX = sizeX + 1;
        int vSizeZ = sizeZ + 1;
        int numVerts = vSizeX * vSizeZ;

        //Generate mesh data
        Vector3[] vertices = new Vector3[numVerts];
        Vector3[] normals = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];

        int[] triangles = new int[numTris * 3];

        int x, z;
        for (z = 0; z < vSizeZ; z++)
        {
            for (x = 0; x < vSizeX; x++)
            {
                vertices[z * vSizeX + x] = new Vector3(x * tileSize - sizeX * 0.5f, 0, z * tileSize - sizeZ * 0.5f);
                normals[z * vSizeX + x] = Vector3.up;
                uv[z * vSizeX + x] = new Vector2((float)x / sizeX, (float)z / sizeZ);
            }
        }

        for(z = 0; z < sizeZ; z++)
        {
            for (x = 0; x < sizeX; x++)
            {
                Vector3 tileCenter = GetTileCenter(x, z);
                TDTile tile = new TDTile();
                tile.posX = tileCenter.x - sizeX * 0.5f;
                tile.posY = tileCenter.y;
                tile.posZ = tileCenter.z - sizeZ * 0.5f;
                map.tiles[x, z] = tile;

                int squareIndex = z * sizeX + x;
                int triOffset = squareIndex * 6;
                triangles[triOffset + 0] = z * vSizeX + x + 0;
                triangles[triOffset + 1] = z * vSizeX + x + vSizeX;
                triangles[triOffset + 2] = z * vSizeX + x + vSizeX + 1;

                triangles[triOffset + 3] = z * vSizeX + x + 0;
                triangles[triOffset + 4] = z * vSizeX + x + vSizeX + 1;
                triangles[triOffset + 5] = z * vSizeX + x + 1;
            }
        }

        //create mesh and asign data
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;
        mesh.uv = uv;

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
        meshRenderer.material = groundMat;
        for (int i = 0; i < map.width; i++)
        {
            for(int j = 0; j < map.height; j++)
            {
                Debug.Log(map.GetTile(i, j).type);
            }
        }
        PopulateChunk();
    }

    Vector3 GetTileCenter(float x, float z)
    {
        Vector3 center = new Vector3((x + 0.5f) * tileSize, 0, (z + 0.5f) * tileSize); 
        return center;
    }

    void PopulateChunk()
    {
        int spawn;
        GameObject spawnedObject;

        for(int x = 0; x < sizeX; x++)
        {
            for(int z = 0; z < sizeZ; z++)
            {
                spawn = Random.Range(0, 101);
                if(spawn < 5)
                {
                    spawnedObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    map.tiles[x, z].type = TDTile.TILE_FARMLAND;
                }
                else if(spawn > 50 && spawn < 55)
                {
                    spawnedObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                }
                else
                {
                    spawnedObject = null;
                }

                if(spawnedObject != null)
                {
                    //pawnedObject.GetComponent<Collider>().isTrigger = true;
                    spawnedObject.transform.SetParent(transform);
                    map.tiles[x, z].obstacle = true;
                    spawnedObject.transform.localPosition = new Vector3(map.tiles[x, z].posX, 0 + spawnedObject.GetComponent<Collider>().bounds.size.y * 0.5f, map.tiles[x, z].posZ);
                    spawnedObject.transform.rotation = Quaternion.Euler(0, 45, 0);
                }
            }
        }
    }
}
