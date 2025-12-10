using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public static MovimientoJugador instance;

    private Rigidbody2D playerRb; //rigidbody del jugador
    
    public float movimientoEjeX;
    public float velocidadPlayer = 8f;

    private float limiteXMax = 2.696f;
    private float limiteXMin = -2.696f;

    /// <summary>
    /// PowerUp Settings
    /// </summary>
    public float multiplicadorTamano = 1.8f;
    public float duracionPowerUp = 5f;

    private float tiempoFinEfectoTamano = 0f;
    private float tiempoFinEfectoInvertido = 0f;
    private Vector3 tamanoInicial;
    public bool movimientoInvertido = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        playerRb = GetComponent<Rigidbody2D>(); //obtenemos el rigidbody del jugador
    }

    private void Start()
    {
        tamanoInicial = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(tiempoFinEfectoTamano > 0 && Time.time >= tiempoFinEfectoTamano)
        {
            tiempoFinEfectoTamano = 0;
            transform.localScale = tamanoInicial;
            Debug.Log("Efecto tamano terminado");
        }

        if (tiempoFinEfectoInvertido > 0 && Time.time >= tiempoFinEfectoInvertido)
        {
            tiempoFinEfectoInvertido = 0;
            movimientoInvertido = false;
            Debug.Log("Movimiento Invertido terminado.");
        }

        movimientoEjeX = Input.GetAxis("Horizontal"); //obtenemos el movimiento en el eje X
        
        if (movimientoInvertido)
        {
            movimientoEjeX = -movimientoEjeX; // Invertir el eje X
        }
    }

    void FixedUpdate()
    {
        if (playerRb == null) return;

        /*float moveDistance = movimientoEjeX * velocidadPlayer * Time.fixedDeltaTime; //calculamos la velocidad en el eje X
        float nuevaPosX = playerRb.position.x + moveDistance; //calculamos la nueva posicion en el eje X
        
        Vector2 targetPosition = new Vector2(nuevaPosX, playerRb.position.y);
        playerRb.MovePosition(targetPosition);//movemos al jugador a la nueva posicion*/

        Vector2 velocidadMovimiento = new Vector2(movimientoEjeX * velocidadPlayer, 0);
        playerRb.linearVelocity = velocidadMovimiento;

        
        /*
        Vector2 posicionActual = playerRb.position;

        float nuevaXClamp = Mathf.Clamp(posicionActual.x, limiteXMin, limiteXMax);

        Vector2 posicionRestringida = new Vector2(nuevaXClamp, posicionActual.y);
        playerRb.MovePosition(posicionRestringida);*/
    }

    public void ActivarAumentoTamano()//powerup tamano
    {
        transform.localScale = tamanoInicial * multiplicadorTamano;
        tiempoFinEfectoTamano = Time.time + duracionPowerUp;
    }

    public void ActivarMovimientoInvertido()
    {
        movimientoInvertido = true;
        tiempoFinEfectoInvertido = Time.time + duracionPowerUp;
    }
}
