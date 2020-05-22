using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public int uvAnimationTileX = 24;
    public int uvAnimationTileY = 1;
    public int framesPerSecond = 10;
    private MeshRenderer render;

    private void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        int index = (int)(Time.time * framesPerSecond);
        index = index % (uvAnimationTileX * uvAnimationTileY);

        Vector2 size = new Vector2(1.0f / uvAnimationTileX, 1.0f / uvAnimationTileY);

        int uIndex = index % uvAnimationTileX;
        int vIndex = index / uvAnimationTileX;

        // build offset
        // v coordinate is the bottom of the image in opengl so we need to invert.
        var offset = new Vector2(uIndex * size.x, 1.0f - size.y - vIndex * size.y);

        render.material.SetTextureOffset("_MainTex", offset);
        render.material.SetTextureScale("_MainTex", size);
    }
}
