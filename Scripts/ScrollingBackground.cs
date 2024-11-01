using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public Renderer firstBackground;    
    public Renderer secondBackground;    
    public float scrollSpeed = 2f;    
    private float backgroundWidth;    

    void Start()
    {
        backgroundWidth = firstBackground.GetComponent<Renderer>().bounds.size.x;
    }

    void Update()
    {
        firstBackground.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        secondBackground.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        if (firstBackground.transform.position.x <= -backgroundWidth)
        {
            firstBackground.transform.position = new Vector3(
                secondBackground.transform.position.x + backgroundWidth, 
                firstBackground.transform.position.y,
                firstBackground.transform.position.z);

            SwapBackgrounds();
        }
    }

    void SwapBackgrounds()
    {
        Renderer temp = firstBackground;
        firstBackground = secondBackground;
        secondBackground = temp;
    }
}

