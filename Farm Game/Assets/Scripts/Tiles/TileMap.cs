using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer)), RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour
{
    public int sizeX;
    public int sizeZ;
    public float tileSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BuildMesh(sizeX, sizeZ, tileSize);
    }


    public void BuildMesh(int sizeX, int sizeZ, float tileSize)
    {
        TDMap map = new TDMap(sizeX, sizeZ);
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
                vertices[z * vSizeX + x] = new Vector3(x * tileSize, 0, z * tileSize);
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
                tile.posX = tileCenter.x;
                tile.posY = tileCenter.y;
                tile.posZ = tileCenter.z;
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
        for (int i = 0; i < map.width; i++)
        {
            for(int j = 0; j < map.height; j++)
            {
                Debug.Log(map.GetTile(i, j).type);
            }
        }
    }

    Vector3 GetTileCenter(float x, float z)
    {
        Vector3 center = new Vector3((x + 0.5f) * tileSize, 0, (z + 0.5f) * tileSize); 
        return center;
    }
}
