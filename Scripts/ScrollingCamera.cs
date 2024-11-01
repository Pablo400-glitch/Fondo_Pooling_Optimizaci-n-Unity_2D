using System.Collections;using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollingCamera : MonoBehaviour
{
    public Renderer firstBackground;
    public Renderer secondBackground;
    public float scrollSpeed = 2f;

    private float backgroundWidth;                    
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        backgroundWidth = firstBackground.GetComponent<Renderer>().bounds.size.x;
    }

    void Update()
    {
        _camera.transform.position += Vector3.right * scrollSpeed * Time.deltaTime;

        // Cuando la posición del lado izquierdo hasta donde se ve la camara es mayor o igual que la posición del lado derecho del primer fondo
        if (firstBackground.transform.position.x + (backgroundWidth / 2) <= _camera.transform.position.x - (_camera.orthographicSize * _camera.aspect))
        {
            Debug.Log(firstBackground.transform.position.x + (backgroundWidth / 2));
            Debug.Log(_camera.transform.position.x - (_camera.orthographicSize * _camera.aspect));
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
