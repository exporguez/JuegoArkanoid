using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public static PowerUps instance;
    public enum TipoPowerUp { AumentarTamano, MultiBola, MovimientoInvertido} //SuperFuerza}
    public TipoPowerUp tipo;
    public float velocidadCaida = 3f;

    public AudioClip powerUpSound;
    public float powerUpSoundVolume = 2.0f;

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
            ReproducirPremio();
            AplicarEfecto();
            Destroy(gameObject);
        }
        
        if(other.CompareTag("Vacio"))
        {
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
                /*case TipoPowerUp.SuperFuerza:
                    bola.ActivarSuperFuerza();
                    break;*/
            }
        }
    }

    public void ReproducirPremio()
    {
        AudioSource.PlayClipAtPoint(powerUpSound, transform.position, powerUpSoundVolume);
    }

    public void DestruirPowerUps()
    {
        Destroy(gameObject);
    }
}
