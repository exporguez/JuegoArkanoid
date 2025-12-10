using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public enum TipoPowerUp { AumentarTamano, MultiBola, MovimientoInvertido, SuperFuerza}
    public TipoPowerUp tipo;
    public float velocidadCaida = 3f;

    private void Update()
    {
        transform.Translate(Vector3.down * velocidadCaida * Time.deltaTime);

        if(transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Jugador"))
        {
            AplicarEfecto();
            Destroy(gameObject);
        }    
    }

    void AplicarEfecto()
    {
        MovimientoJugador jugador = FindObjectOfType<MovimientoJugador>();
        BallController bola = FindObjectOfType<BallController>();

        if(jugador != null)
        {
            switch (tipo)
            {
                case TipoPowerUp.AumentarTamano:
                    jugador.ActivarAumentoTamano();
                    break;
                case TipoPowerUp.MovimientoInvertido:
                    jugador.ActivarMovimientoInvertido();
                    break;
            }
        }

        if(bola != null)
        {
            switch (tipo)
            {
                case TipoPowerUp.MultiBola:
                    bola.ActivarMultiBola();
                    break;
                case TipoPowerUp.SuperFuerza:
                    bola.ActivarSuperFuerza();
                    break;
            }
        }
    }
}
