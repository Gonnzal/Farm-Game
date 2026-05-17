using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChunckManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject chunk;
    List <GameObject> generated = new List<GameObject>();
    List <Vector3> positions = new List<Vector3>();
    List <GameObject> temporal = new List<GameObject>();
    [SerializeField] float recalculate;
    [SerializeField] float activationDistance;
    float offset;
    GameObject temp;
    float currentTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        temp = GameObject.Instantiate(chunk, transform.position, transform.rotation);
        offset = temp.GetComponent<TileMap>().sizeX * temp.GetComponent<TileMap>().tileSize;
        generated.Add(temp);
        positions.Add(temp.transform.position);
        temp.transform.SetParent(transform);
        Check();
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= recalculate)
        {
            currentTime = 0;
            Check();
        }
    }

    void Check()
    {
        Vector3 tempPos;
        foreach(GameObject obj in generated)
        {
            Vector3[] neighbours = {
                obj.transform.position + new Vector3(offset, 0, 0),
                obj.transform.position + new Vector3(-offset, 0, 0),
                obj.transform.position + new Vector3(0, 0, offset),
                obj.transform.position + new Vector3(0, 0, -offset)
            };

            foreach (var pos in neighbours)
            {
                if(!positions.Contains(pos))
                {
                    if(Distance(player.transform.position, pos) > activationDistance)
                    {
                        continue;
                    }
                    else
                    {
                        temp = Instantiate(chunk, pos, Quaternion.Euler(0, 0, 0));
                        temporal.Add(temp);
                        positions.Add(temp.transform.position);
                        temp.transform.SetParent(transform);
                    }
                }
            }

            if(Distance(player.transform.position, obj.transform.position) >= activationDistance)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
            }
        }
        for(int i = 0; i < temporal.Count; i++)
        {
            generated.Add(temporal[i]);
        }
        temporal.Clear();
    }

    float Distance(Vector3 player, Vector3 chunk)
    {
        float distanceX, distanceZ, distance;
        distanceX = Mathf.Abs(player.x - chunk.x);
        distanceZ = Mathf.Abs(player.z - chunk.z);
        distance = Mathf.Sqrt(distanceX * distanceX + distanceZ * distanceZ);
        return distance;
    }
}
