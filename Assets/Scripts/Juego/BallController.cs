using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController instance;
    private MovimientoJugador jugador;
    public float velocidadInicial = 2f;// Velocidad inicial de la bola
    public GameObject ball; // Objeto bola

    private Rigidbody2D ballRb; // Rigidbody de la bola
    private bool gameStarted = false;// Indica si el juego ha comenzado
    

    private float offsetY = 0.5f;

    private float maxReboteFactor = 1.5f; // Factor maximo de rebote en la pala

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        ballRb = ball.GetComponent<Rigidbody2D>();// Obtenemos el Rigidbody2D de la bola
        jugador = FindFirstObjectByType<MovimientoJugador>();
        
    }
   
    void Update()
    {
        if (!gameStarted)
        {
            PosicionarSobreJugador(); // Mantenemos la bola en la posici�n del jugador con el offset

            if (Input.GetButtonDown("Jump")) // Iniciamos el juego al presionar el bot�n de salto
            {
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
        
        if(!gameStarted)
        {
            if(Cronometro.instance != null)
            {
                Cronometro.instance.IniciarCronometro();
            }
        }
        gameStarted = true;

        Vector2 lanzamientoDireccion = new Vector2(Random.Range(-1f, 1f), 1).normalized; // Direcci�n aleatoria hacia arriba
        ballRb.linearVelocity = lanzamientoDireccion * velocidadInicial; // Asignamos la velocidad inicial a la bola
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameStarted) return;

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

        ballRb.linearVelocity = nuevaDireccion * velocidadInicial;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Vacio"))
        {
            Vidas.instance.PerderVidas();
            ReiniciarPelota();
        }
    }

    public void ReiniciarPelota()
    {
        gameStarted = false;
        ballRb.linearVelocity = Vector2.zero;
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
}

    

