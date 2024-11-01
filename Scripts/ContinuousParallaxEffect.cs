using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ContinuousParallaxEffect : MonoBehaviour
{
    private Material[] Layers;
    public float scrollSpeed1 = 0.1f;
    public float scrollSpeed2 = 0.05f;

    private Vector2 offset1 = Vector2.zero;
    private Vector2 offset2 = Vector2.zero;

    void Start()
    {
        Layers = GetComponent<Renderer>().materials;
    }

    void Update()
    {
        offset1.x += scrollSpeed1 * Time.deltaTime;
        offset2.x += scrollSpeed2 * Time.deltaTime;

        Layers[0].SetTextureOffset("_MainTex", offset1);
        Layers[1].SetTextureOffset("_MainTex", offset2);
    }
}
