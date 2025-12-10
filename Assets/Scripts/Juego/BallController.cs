using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    public static BallController instance;
    private MovimientoJugador jugador;
    public float velocidadInicial = 2f;// Velocidad inicial de la bola
    public GameObject ball; // Objeto bola

    private Rigidbody2D ballRb; // Rigidbody de la bola
    private bool gameStarted = false;// Indica si el juego ha comenzado

    public static GameObject ballPrefabToInstantiate;
    public static Vector3 playerInitialPosition;

    /// <summary>
    /// PowerUps Settings
    /// </summary>
    public GameObject bolaPrefab;
    public bool superFuerzaActiva = false;
    public float duracionPowerUps = 5f;

    private float offsetY = 0.5f;

    private float maxReboteFactor = 1.5f; // Factor maximo de rebote en la pala

    public AudioClip bounceSound;
    public float bounceVolume = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (ball != null)
        {
            ballRb = ball.GetComponent<Rigidbody2D>();// Obtenemos el Rigidbody2D de la bola
        }


        jugador = FindFirstObjectByType<MovimientoJugador>();

    }

    void Update()
    {
        if (!gameStarted)
        {
            PosicionarSobreJugador(); // Mantenemos la bola en la posici�n del jugador con el offset

            if (Input.GetButtonDown("Jump")) // Iniciamos el juego al presionar el bot�n de salto
            {
                if (ControlMenus.Instance != null && ControlMenus.Instance.popUpJugar != null)
                {
                    ControlMenus.Instance.popUpJugar.SetActive(false);
                }
                Lanzar();
            }
        }
    }

    void PosicionarSobreJugador()
    {
        if (jugador == null) return;

        var playerPos = jugador.transform.position;

        transform.position = playerPos + new Vector3(0, offsetY, 0);
    }


    public void Lanzar() // Funci�n para lanzar la bola
    {
        if (ballRb == null) return;

        if (!gameStarted)
        {
            if (Cronometro.instance != null)
            {
                Cronometro.instance.IniciarCronometro();
            }
        }
        gameStarted = true;

        Vector2 lanzamientoDireccion = new Vector2(Random.Range(-1f, 1f), 1).normalized; // Direcci�n aleatoria hacia arriba
        ballRb.linearVelocity = lanzamientoDireccion * velocidadInicial; // Asignamos la velocidad inicial a la bola
    }

    public void ActivarMultiBola()
    {
        // Si tienes un Prefab de la bola
        if (bolaPrefab == null)
        {
            Debug.LogError("¡Asigna el Prefab de la bola en el Inspector!");
            return;
        }

        // Crear dos copias de la bola actual
        InstanciarNuevaBola(bolaPrefab, new Vector2(0.5f, 1f));
        InstanciarNuevaBola(bolaPrefab, new Vector2(-0.5f, 1f));
    }

    void InstanciarNuevaBola(GameObject prefab, Vector2 direccion)
    {
        GameObject nuevaBola = Instantiate(prefab, transform.position, Quaternion.identity);
        BallController nuevaBolaController = nuevaBola.GetComponent<BallController>();
        if (nuevaBolaController != null)
        {
            nuevaBolaController.gameStarted = true;
        }

        Rigidbody2D nuevaBolaRb = nuevaBola.GetComponent<Rigidbody2D>();
        nuevaBolaRb.linearVelocity = direccion.normalized * ballRb.linearVelocity.magnitude;

    }

    public void ActivarSuperFuerza()
    {
        superFuerzaActiva = true;
        // La SuperFuerza durará 10 segundos
        StartCoroutine(DesactivarSuperFuerzaDespuesDe(duracionPowerUps));
    }

    IEnumerator DesactivarSuperFuerzaDespuesDe(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        superFuerzaActiva = false;
        Debug.Log("SuperFuerza terminada.");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameStarted) return;

        ReproducirRebote();

        if (collision.gameObject.CompareTag("Bloque"))
        {
            BricksController bloque = collision.gameObject.GetComponent<BricksController>();

            if (bloque != null)
            {
                /*if (superFuerzaActiva)
                {
                    collision.collider.enabled = false;
                    bloque.DestroyBlock();
                }
                else
                {
                    bloque.HitBlock();
                }*/
                bloque.HitBlock();
            }
            return;

        }

        Vector2 direccionActual = ballRb.linearVelocity.normalized;
        Vector2 nuevaDireccion;

        if (collision.gameObject.CompareTag("Jugador"))
        {
            nuevaDireccion = RebotePala(collision);
        }
        else
        {
            nuevaDireccion = direccionActual;
            AjustarAngulo(ref nuevaDireccion);
        }

        float velocidadMantenida = ballRb.linearVelocity.magnitude;

        if (velocidadMantenida < velocidadInicial)
        {
            velocidadMantenida = velocidadInicial;
        }
        ballRb.linearVelocity = nuevaDireccion * velocidadMantenida;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vacio"))
        {
            BallController[] todasLasBolas = FindObjectsOfType<BallController>();
            int bolasRestantes = todasLasBolas.Length;

            if (bolasRestantes <= 1)
            {
                Vidas.instance.PerderVidas();
                ReiniciarPelota();
            }
            else
            {
                Destroy(gameObject);

            }

            /*if (other.CompareTag("Bloque"))
            {
                BricksController bloque = other.GetComponent<BricksController>();

                if (bloque != null && superFuerzaActiva)
                {
                    bloque.DestroyBlock();
                }
            }*/
        }
    }

    public void ReiniciarPelota()
    {
        gameStarted = false;

        if (ballRb != null)
        {
            ballRb.linearVelocity = Vector2.zero;
        }

        PosicionarSobreJugador();
    }

    void AjustarAngulo(ref Vector2 velocidadActual)
    {
        const float minAngle = 0.2f; // �ngulo m�nimo en grados

        if (Mathf.Abs(velocidadActual.x) < minAngle)
        {
            velocidadActual.x = Mathf.Sign(velocidadActual.x) * minAngle;
        }

        if (Mathf.Abs(velocidadActual.y) < minAngle)
        {
            velocidadActual.y = Mathf.Sign(velocidadActual.y) * minAngle;
        }

        velocidadActual = velocidadActual.normalized;
    }

    private Vector2 RebotePala(Collision2D collision)
    {
        Vector2 hitPoint = collision.contacts[0].point;

        Collider2D collider2D = collision.collider;

        if (collider2D == null) return ballRb.linearVelocity.normalized;

        float hitOffset = collider2D.bounds.center.x - hitPoint.x;

        float width = collider2D.bounds.size.x;

        float hitX = -hitOffset / (width / 2f);

        Vector2 nuevaDireccion = new Vector2(
            hitX * maxReboteFactor,
            1f
        );

        return nuevaDireccion.normalized;
    }

    public static void DestruirTodasLasBolas()
    {
        if (instance != null)
        {
            if(instance.jugador != null)
            {
                BallController.PrepararSiguienteBola(instance.bolaPrefab, instance.jugador.transform.position);
            }
            else
            {
                BallController.PrepararSiguienteBola(instance.bolaPrefab, Vector3.zero);
            }
        }

        BallController[] todasLasBolas = FindObjectsOfType<BallController>();

        foreach (BallController bolas in todasLasBolas)
        {
            Destroy(bolas.gameObject);
        }
        instance = null;
    }

    public static void PrepararSiguienteBola(GameObject prefab, Vector3 playerPos)
    {
        ballPrefabToInstantiate = prefab;
        playerInitialPosition = playerPos;
    }

    public static void ReinstanciarBola()
    {
        if (ballPrefabToInstantiate != null)
        {
            Instantiate(ballPrefabToInstantiate, playerInitialPosition, Quaternion.identity);
        }
    }

    public void ReproducirRebote()
    {
        if (bounceSound != null)
        {
            AudioSource.PlayClipAtPoint(bounceSound, transform.position, bounceVolume);
        }            
    }
}



