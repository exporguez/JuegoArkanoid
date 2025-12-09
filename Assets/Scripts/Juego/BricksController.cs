using UnityEngine;

public class BricksController : MonoBehaviour
{
    public Sprite[] blockState;

    public GameObject blockPrefab;

    public int scoreValor = 100;

    private SpriteRenderer spriteRenderer;
    private int hitCount = 0;
    private int maxHits;

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

        if(Score.instance != null)
        {
            Score.instance.BloqueCreado();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Pelota"))
        {
            HitBlock();
        }
    }

    public void HitBlock()// Golpear el bloque
    {
        hitCount++;

        if (Score.instance != null)
        {
            Score.instance.SumarPuntos(scoreValor);
        }

        if (blockPrefab != null)
        {
            Instantiate(blockPrefab, transform.position, Quaternion.identity);
        }

        if (hitCount >= maxHits)
        {
            if(maxHits > 0)
            {
                //spriteRenderer.sprite = blockState[blockState.Length - 1];                
            }
            DestroyBlock();
        }
        else
        {
            UpdateSprite();
        }
    }

    public void UpdateSprite()// Actualizar el sprite del bloque segun los golpes recibidos
    {
        int spriteIndex = hitCount - 1;

        if (spriteIndex >= 0 && spriteIndex < blockState.Length)
        {
            spriteRenderer.sprite = blockState[spriteIndex];
        }
    }

    public void DestroyBlock()// Destruir el bloque
    {
        /*Anadir efectod de sonido, particulas, powerups, sumar puntos*/

        if(Score.instance != null)
        {
            Score.instance.BloqueDestruido();
        }

        Destroy(gameObject);
    }

      
}
