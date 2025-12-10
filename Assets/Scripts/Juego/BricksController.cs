using UnityEngine;
using System.Collections;

public class BricksController : MonoBehaviour
{
    public Sprite[] blockState;

    public GameObject blockPrefab;

    public int scoreValor = 100;

    public int limiteNoPowerUp = 9; // si el número aleatorio es menor a 9 no sale nada
    /// <summary>
    /// Números que definen cada powerup
    /// </summary>
    public int rangoPowerUp_1 = 6;
    public int rangoPowerUp_2 = 7;
    public int rangoPowerUp_3 = 8;
    public int rangoPowerUp_4 = 9;

    public GameObject[] powerUpPrefabs;

    private SpriteRenderer spriteRenderer;
    private int hitCount = 0;
    private int maxHits;

    private bool isHit = false;

    public float duracionTemblor = 0.1f;
    public float magnitudTemblor = 0.1f;
    public int temblorFrecuencia = 10;

    public GameObject particulas;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        maxHits = blockState.Length;

        if (maxHits > 0)
        {
            //spriteRenderer.sprite = blockState[0];
        }
        else
        {
            maxHits = 1;
        }

        if (Score.instance != null)
        {
            Score.instance.BloqueCreado();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pelota"))
        {
            HitBlock();
        }
    }

    public void HitBlock() // Golpear el bloque
    {
        if (isHit) return;
        isHit = true;

        hitCount++;

        if (Score.instance != null)
        {
            Score.instance.SumarPuntos(scoreValor);
        }

        /*if (blockPrefab != null)
        {
            Instantiate(blockPrefab, transform.position, Quaternion.identity);
        }*/

        AnimarTemblor();

        if (hitCount >= maxHits)
        {
            if (maxHits > 0)
            {
                //spriteRenderer.sprite = blockState[blockState.Length - 1];                
            }

            DestroyBlock();
        }
        else
        {
            UpdateSprite();
        }

        StartCoroutine(ResetHitProcessing());
    }

    IEnumerator ResetHitProcessing()
    {
        // Esperar un frame de física para evitar el doble golpe.
        yield return new WaitForFixedUpdate();
        isHit = false;
    }

    public void UpdateSprite() // Actualizar el sprite del bloque según los golpes recibidos
    {
        int spriteIndex = hitCount - 1;

        if (spriteIndex >= 0 && spriteIndex < blockState.Length)
        {
            spriteRenderer.sprite = blockState[spriteIndex];
        }
    }

    public void DestroyBlock() // Destruir el bloque
    {
        // Efecto de partículas
        InstanciarParticulas(transform.position);

        // Powerups
        GenerarPowerUp();

        if (Score.instance != null)
        {
            Score.instance.BloqueDestruido();
        }

        Destroy(gameObject);
    }

    void GenerarPowerUp()
    {
        if (powerUpPrefabs.Length < 4)
        {
            Debug.LogWarning("Se requieren 4 prefabs de powerup en powerUpPrefabs.");
            return;
        }

        int numAleatorio = Random.Range(1, 11);

        if (numAleatorio < limiteNoPowerUp)
        {
            return;
        }

        GameObject powerUpAInstanciar = null;

        if (numAleatorio >= rangoPowerUp_4)
        {
            powerUpAInstanciar = powerUpPrefabs[3];
        }
        else if (numAleatorio == rangoPowerUp_3)
        {
            powerUpAInstanciar = powerUpPrefabs[2];
        }
        else if (numAleatorio == rangoPowerUp_2)
        {
            powerUpAInstanciar = powerUpPrefabs[1];
        }
        else if (numAleatorio == rangoPowerUp_1)
        {
            powerUpAInstanciar = powerUpPrefabs[0];
        }

        if (powerUpAInstanciar != null)
        {
            Instantiate(powerUpAInstanciar, transform.position, Quaternion.identity);
        }
    }

    void AnimarTemblor()
    {
        LeanTween.cancel(gameObject);

        Vector3 posicionOriginal = transform.localPosition;

        LeanTween.value(
            gameObject,
            0f,
            1f,
            duracionTemblor
        )
        .setOnUpdate((float t) =>
        {
            float x = Mathf.Sin(t * temblorFrecuencia) * magnitudTemblor;
            float y = Mathf.Sin(t * temblorFrecuencia * 1.5f) * magnitudTemblor * 0.5f;

            transform.localPosition = posicionOriginal + new Vector3(x, y, 0) * (1f - t);
        })
        .setOnComplete(() =>
        {
            transform.localPosition = posicionOriginal;
        });
    }

    public void InstanciarParticulas(Vector3 posicion) // Instancia el efecto de partículas en la posición dada
    {
        if (particulas != null)
        {
            GameObject efecto = Instantiate(particulas, posicion, Quaternion.identity);

            Animator anim = efecto.GetComponent<Animator>();

            if (anim != null)
            {
                AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
                
                float duracionAnim = 0.5f;

                if (clipInfo != null && clipInfo.Length > 0 && clipInfo[0].clip != null)
                {
                    duracionAnim = clipInfo[0].clip.length;
                }

                Destroy(efecto, duracionAnim + 0.1f);
            }
            else
            {
                Destroy(efecto, 0.5f);
            }
        }
    }

    /*public void DestruirParticulas(GameObject particulas)
    {
        Destroy(particulas);
    }*/
}
