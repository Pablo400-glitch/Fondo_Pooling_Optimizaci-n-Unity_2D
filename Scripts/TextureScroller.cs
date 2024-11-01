using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5f; 
    private Renderer background;

    void Start()
    {
        background = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        background.material.mainTextureOffset = new Vector2(offset, 0);
    }
}