# Fondo, Pooling y Optimización en Unity - FDV

## Tarea 1: Aplicar un fondo con scroll a tu escena utilizando la técnica descrita en a.

En esta tarea, la cámara se mantiene fija en su posición mientras el fondo se desplaza para crear un efecto de movimiento. El fondo se mueve hacia la izquierda de la cámara y, al alcanzar una posición específica, se reposiciona justo a la derecha del fondo visible para el jugador en ese momento.

El movimiento se gestiona mediante el script ```ScrollingBackground```, donde los métodos más relevantes son `Update()` y `SwapBackgrounds`. En el método `Update()`, ambos fondos se desplazan hacia la izquierda. Cuando el primer fondo alcanza una posición determinada a la izquierda, se reposiciona en la parte derecha del segundo fondo. Posteriormente, se utiliza el método `SwapBackgrounds` para intercambiar el primer fondo por el segundo y viceversa, replicando así el comportamiento anterior con el fondo alternativo.

```csharp
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
```

![Descripción de la imagen](images/1.1.gif)

*Figura 1: Desplazamiento del fondo en la escena*

![Descripción de la imagen](images/1.gif)

*Figura 2: Desplazamiento del fondo en el juego*

## Tarea 2: Aplicar un fondo con scroll a tu escena utilizando la técnica descrita en b.

En esta tarea, el fondo se mantiene fijo en su posición mientras la cámara se desplaza para crear un efecto de movimiento. Cuando la posición del lado izquierdo visible de la cámara es mayor o igual que la posición del lado derecho del primer fondo, el primer fondo se reposiciona a la derecha del segundo fondo.

El movimiento se controla a través del script ```ScrollingBackground```, que utiliza los métodos `Update()` y `SwapBackgrounds`. En `Update()`, la cámara se desplaza hacia la derecha. Cuando el lado izquierdo visible de la cámara alcanza el lado derecho del primer fondo, este se reposiciona a la derecha del segundo fondo. Luego, se llama a `SwapBackgrounds` para intercambiar las referencias de los fondos, manteniendo así el ciclo de movimiento continuo.


```csharp
void Start()
{
    _camera = Camera.main;
    backgroundWidth = firstBackground.GetComponent<Renderer>().bounds.size.x;
}

void Update()
{
    _camera.transform.position += Vector3.right * scrollSpeed * Time.deltaTime;

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
```

![Descripción de la imagen](images/2.gif)

*Figura 3: Desplazamiento de la camara de juego*

## Tarea 3: Aplicar un fondo a tu escena aplicando la técnica del desplazamiento de textura

En esta actividad el fondo se moverá aplicando el desplazamiento de textura, la camara y el fondo se quedarán en la misma posición. 

Para poder hacer este desplazamiento de textura el fondo tiene que tener activa en su textura la propiedad **Wrap mode** en **Repeat** para que el fondo se pueda repetir de manera infinita a la izquierda o a la derecha. 

Este efecto es controlado por el script ```TextureScroller```. Lo más destable es un método `Update()`, donde calcula un desplazamiento (offset) basado en el tiempo transcurrido y una variable ```scrollSpeed```. Luego, ajusta la propiedad ```mainTextureOffset``` del material para desplazar la textura horizontalmente, creando el efecto de movimiento fluido.

```csharp
void Start()
{
    background = GetComponent<Renderer>();
}

void Update()
{
    float offset = Time.time * scrollSpeed;
    background.material.mainTextureOffset = new Vector2(offset, 0);
}
```

![Descripción de la imagen](images/3.gif)

*Figura 4: Desplazamiento de textura*

## Tarea 4: Aplicar efecto parallax usando la técnica de scroll en la que se mueve continuamente la posición del fondo.

En está actividad hay que mover dos fondos, estos fondos tienen velocidades distintas para aplicar el efecto de paralax.

En el script de ```ContinuousParallaxEffect``` se aplica un **efecto parallax** utilizando la técnica de **scroll**, donde la posición de las texturas de fondo se mueve continuamente. 

- **Variables**: 
    - **`Layers`** almacena los materiales del objeto.
    - **`offset1`** y **`offset2`** definen el offset de las texturas de los fondos.
    - **`scrollSpeed1`** y **`scrollSpeed2`** establecen las velocidades de desplazamiento de cada fondo.

- **Metodos**: 
    - **`Update()`**: En este método se incrementan los offsets de las texturas (`offset1` y `offset2`) en función del tiempo y de las velocidades definidas (`scrollSpeed1` y `scrollSpeed2`). Esto provoca que las capas de fondo se desplacen a diferentes velocidades, simulando una mayor profundidad. Las texturas se actualizan mediante `SetTextureOffset`, lo que crea la ilusión de movimiento en el fondo, mejorando la experiencia visual del juego.

```csharp
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
```

![Descripción de la imagen](images/4.gif)

*Figura 5: Efecto parallax usando la técnica scroll*

## Tarea 5: Aplicar efecto parallax actualizando el offset de la textura.

En la actividad anterior, el desplazamiento de las texturas se mantenía uniforme; sin embargo, en el script desarollado en esta actividad, las capas de fondo que están más cerca del jugador se mueven más rápidamente que las que están más atrás.

El script implementa un **efecto paralax** desplazando continuamente las texturas de múltiples capas a diferentes velocidades. 

- **Variables**: 
    - **`baseSpeed`** define la velocidad base de desplazamiento.
    - **`Layers`** almacena los materiales del objeto.
    - **`speedOffset`** ajusta la velocidad de cada capa según su índice.
- **Metodos**: 
    - **`Update()`**: En este método a través de un bucle, se calcula un nuevo desplazamiento para cada capa. El desplazamiento se ajusta en función del índice de la capa, lo que permite que las capas más cercanas se muevan más rápido que las más alejadas, creando una ilusión de profundidad.

```csharp
public Vector2 baseSpeed = new Vector2(0.5f, 0.0f); 
private Material[] Layers;                          
public float speedOffset = 0.1f;                    

void Start()
{
    Layers = GetComponent<Renderer>().materials;
}

void Update()
{
    for (int i = 0; i < Layers.Length; i++)
    {
        Material m = Layers[i];
        Vector2 newOffset = m.GetTextureOffset("_MainTex") + baseSpeed * (speedOffset / (i + 1.0f)) * Time.deltaTime;
        m.SetTextureOffset("_MainTex", newOffset);
    }
}
```

![Descripción de la imagen](images/5.gif)

*Figura 6: Efecto parallax actualizando el offset*

## Tarea 6

### Enunciado

En tu escena 2D crea un prefab que sirva de base para generar un tipo de objetos sobre los que vas a hacer un pooling de objetos que se recolectarán continuamente en tu escena. Cuando un objeto es recolectado debe pasar al pool y dejar de visualizarse. Este objeto estará disponible en el pool. Cada objeto debe llevar un contador, cuando alcance 3 será destruido. En la escena, siempre que sea posible debe haber una cantidad de objetos que fijes, hasta que el número de objetos que no se han eliminado sea menor que dicha cantidad. Recuerda que para generar los objetos puedes usar el método Instantiate. Los objetos ya creados pueden estar activos o no, para ello usar SetActive.

### Resolución

En esta actividad, esarrollé un sistema de pooling en Unity 2D. Este sistema mantiene una cantidad fija de objetos en la escena, reciclando aquellos que ya han sido usados y eliminando de la memoria los que alcanzan un límite de recolección. Los scr ipts `EnemyPooling.cs` y `CollectObjectFromPool.cs` gestionan el ciclo de vida y el comportamiento de los objetos recolectables.

Además de los scripts, creé un prefab de un enemigo utilizando un sprite diseñado que se parece a un pulpo (Figura 6) para la recolección de objetos del pool. Este prefab incluye un `Box Collider 2D`, un `Rigidbody 2D` y una referencia al script `CollectObjectFromPool.cs`.

![Descripción de la imagen](images/6.png)

#### 1. Script `EnemyPooling.cs`

Este script funciona como administrador del pool de objetos y controla la activación, reciclaje y destrucción de los objetos recolectables en la escena. 

- **Inicialización del Pool**: Durante la ejecución, el script crea una cantidad determinada de objetos (según el parámetro `poolSize`) y los desactiva, almacenándolos en una lista llamada `pool`. Estos objetos se mantendrán inactivos hasta ser necesarios en la escena.

- **Activación Controlada de Objetos**: El método `Update()` revisa cuántos objetos recolectables están activos en la escena. Si el número de objetos activos es inferior al límite permitido (`activeObjectLimit`), se activan más objetos desde el pool y se colocan en posiciones aleatorias en la escena.

- **Destrucción de Objetos**: Si un objeto ha sido recolectado tres veces, el método `RemoveAndDestroyObject()` lo elimina tanto del pool como de la escena, liberando memoria y asegurando que no haya referencias activas al mismo. Este evento es registrado en el script `CollectObjectFromPool.cs`.

#### 2. Script `CollectObjectFromPool.cs`

Este script se adjunta a cada pulpo recolectable y gestiona la lógica de recogida, contabilizando cuántas veces ha sido recolectado.

- **Contador de Recolecciones**: Cada vez que el objeto es recogido en la escena, el método `OnPickedUp()` incrementa el contador `pickUpCount`. Si este contador alcanza el límite de 3 recolecciones, el objeto se envía al `ObjectPoolManager` para ser eliminado del pool y destruido.

- **Reutilización del Objeto**: Si el contador no ha alcanzado el límite, el objeto simplemente se desactiva para ser reutilizado más adelante en la escena, optimizando el uso de memoria y procesamiento.

#### Algunas consideraciones

1. **Evitación de Errores de Referencia**: Para evitar errores como `MissingReferenceException`, los objetos no se destruyen directamente en el script `Collectible.cs`. En su lugar, `ObjectPoolManager` se encarga de la eliminación, asegurando que el pool esté siempre actualizado.

Este sistema de pooling es eficiente y reduce la carga de instanciar y destruir objetos continuamente, mejorando así el rendimiento de la aplicación. Además, garantiza un uso óptimo de los recursos al eliminar aquellos objetos que alcanzan su límite de recolección, manteniendo la memoria libre de referencias innecesarias. 

![Descripción de la imagen](images/6.gif)

*Figura 7: Jugador recogiendo objetos del pool*

## Tarea 7: Revisa tu código de la entrega anterior e indica las mejoras que podrías hacer de cara al rendimiento.

Al revisar los scripts de la práctica anterior, he notado que, en general, no presentan grandes problemas de rendimiento. Sin embargo, he identificado algunas optimizaciones que podrían mejorarse. Por ejemplo, en el script de `PlayerMovement`, en lugar de utilizar `GetComponent`, sería más eficiente emplear `SerializeField` en los atributos privados del script.

Existen otras mejoras que podrían implementarse en proyectos futuros, aunque en esta práctica no fueron necesarias. Por ejemplo, la creación de **prefabs** para los niveles no resultó relevante, ya que no era necesario construir niveles adicionales para este caso.
