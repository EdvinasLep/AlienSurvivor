using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    public Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    MovementController mController;
    public GameObject currentChunk;


    void Start()
    {
        mController = FindObjectOfType<MovementController>();
    }

    void Update()
    {
        ChunkChecker();
    }

    void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }

        if (mController.moveDir.x > 0 && mController.moveDir.y == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;  //Right
                SpawnChunk();
            }
        }
        else if (mController.moveDir.x < 0 && mController.moveDir.y == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;  //Left
                SpawnChunk();
            }
        }
        else if (mController.moveDir.y > 0 && mController.moveDir.x == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position;  //Up
                SpawnChunk();
            }
        }
        else if (mController.moveDir.y < 0 && mController.moveDir.x == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;  //Down
                SpawnChunk();
            }
        }
        else if (mController.moveDir.x > 0 && mController.moveDir.y > 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("RightUp").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("RightUp").position;  //RightUp
                SpawnChunk();
            }
        }
        else if (mController.moveDir.x > 0 && mController.moveDir.y < 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("RightDown").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("RightDown").position;  //RightDown
                SpawnChunk();
            }
        }
        else if (mController.moveDir.x < 0 && mController.moveDir.y > 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("LeftUp").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("LeftUp").position;  //LeftUp
                SpawnChunk();
            }
        }
        else if (mController.moveDir.x < 0 && mController.moveDir.y < 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("LeftDown").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("LeftDown").position;  //Up
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);

    }
}