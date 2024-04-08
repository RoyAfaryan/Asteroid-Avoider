using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    public float scrollSpeed = 0.5f;

    private float offset;
    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.mainTexture.wrapMode = TextureWrapMode.Repeat;

        // Start coroutine to gradually increase scroll speed
        StartCoroutine(IncreaseScrollSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }

    private IEnumerator IncreaseScrollSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Wait for 2 seconds

            // Increase scroll speed by 0.1, if it doesn't exceed the maximum speed
            if (scrollSpeed < 2.5f)
            {
                scrollSpeed += 0.1f;
            }
        }
    }
}
