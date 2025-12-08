using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody2D playerRb; //rigidbody del jugador

    public float movimientoEjeX;
    public float velocidadPlayer = 8f;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>(); //obtenemos el rigidbody del jugador
    }
  

    // Update is called once per frame
    void Update()
    {
        movimientoEjeX = Input.GetAxis("Horizontal"); //obtenemos el movimiento en el eje X
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
    }
}
