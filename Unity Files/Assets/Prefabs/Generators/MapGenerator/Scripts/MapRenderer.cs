using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRenderer : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    public Vector2 chunkSize;

    private Vector2 centerSpacement;

    private Vector2 actualCenterPosition = Vector2.zero;
    private Vector2[] nextPossibleChunks = new Vector2[8];

    private MapGenerator mapGenerator;

    private void Awake()
    {
        mapGenerator = this.GetComponent<MapGenerator>();
        centerSpacement = mapGenerator.centerSpacement;
    }

    private void Start()
    {
        QueryChunkUpdate();
    }

    private void Update()
    {
        foreach (Vector2 _possibleChunk in nextPossibleChunks)
        {
            if (cam.transform.position.x > _possibleChunk.x - (chunkSize.x / 2 - centerSpacement.x) &&
                cam.transform.position.x < _possibleChunk.x + (chunkSize.x / 2 - centerSpacement.x) &&
                cam.transform.position.y > _possibleChunk.y - (chunkSize.y / 2 - centerSpacement.y) &&
                cam.transform.position.y < _possibleChunk.y + (chunkSize.y / 2 - centerSpacement.y))
            {
                actualCenterPosition = _possibleChunk;
                QueryChunkUpdate();
            }
        }
    }

    public void QueryChunkUpdate()
    {
        //Reset chunks
        mapGenerator.ResetChunks();

        //Center chunk
        mapGenerator.DrawChunk(actualCenterPosition);

        //Horizontal and Vertical chunks
        mapGenerator.DrawChunk(new Vector2(actualCenterPosition.x + chunkSize.x, actualCenterPosition.y));
        mapGenerator.DrawChunk(new Vector2(actualCenterPosition.x - chunkSize.x, actualCenterPosition.y));
        mapGenerator.DrawChunk(new Vector2(actualCenterPosition.x, actualCenterPosition.y + chunkSize.y));
        mapGenerator.DrawChunk(new Vector2(actualCenterPosition.x, actualCenterPosition.y - chunkSize.y));

        //Diagonal Chunks
        mapGenerator.DrawChunk(new Vector2(actualCenterPosition.x + chunkSize.x, actualCenterPosition.y + chunkSize.y));
        mapGenerator.DrawChunk(new Vector2(actualCenterPosition.x - chunkSize.x, actualCenterPosition.y - chunkSize.y));
        mapGenerator.DrawChunk(new Vector2(actualCenterPosition.x + chunkSize.x, actualCenterPosition.y - chunkSize.y));
        mapGenerator.DrawChunk(new Vector2(actualCenterPosition.x - chunkSize.x, actualCenterPosition.y + chunkSize.y));

        //Get next possible chunks positions
        nextPossibleChunks[0] = new Vector2(actualCenterPosition.x + chunkSize.x, actualCenterPosition.y);
        nextPossibleChunks[1] = new Vector2(actualCenterPosition.x - chunkSize.x, actualCenterPosition.y);
        nextPossibleChunks[2] = new Vector2(actualCenterPosition.x, actualCenterPosition.y + chunkSize.y);
        nextPossibleChunks[3] = new Vector2(actualCenterPosition.x, actualCenterPosition.y - chunkSize.y);
        nextPossibleChunks[4] = new Vector2(actualCenterPosition.x + chunkSize.x, actualCenterPosition.y + chunkSize.y);
        nextPossibleChunks[5] = new Vector2(actualCenterPosition.x - chunkSize.x, actualCenterPosition.y - chunkSize.y);
        nextPossibleChunks[6] = new Vector2(actualCenterPosition.x + chunkSize.x, actualCenterPosition.y - chunkSize.y);
        nextPossibleChunks[7] = new Vector2(actualCenterPosition.x - chunkSize.x, actualCenterPosition.y + chunkSize.y);
    }
}
