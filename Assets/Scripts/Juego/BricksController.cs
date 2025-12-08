using UnityEngine;

public class BricksController : MonoBehaviour
{
    public Sprite[] blockState;

    public GameObject blockPrefab;

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
            spriteRenderer.sprite = blockState[0];
        }
        else
        {
            Debug.LogError("El array esta vacio!");
            Destroy(gameObject);
        }
    }

    public void HitBlock()// Golpear el bloque
    {
        hitCount++;

        if(blockPrefab != null)
        {
            Instantiate(blockPrefab, transform.position, Quaternion.identity);
        }

        if (hitCount >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            UpdateSprite();
        }
    }
    
    public void DestroyBlock()// Destruir el bloque
    {
        /*Anadir efectod de sonido, particulas, powerups, sumar puntos*/

        Destroy(gameObject);
    }

    public void UpdateSprite()// Actualizar el sprite del bloque segun los golpes recibidos
    {
        if (hitCount < maxHits)
        {
            spriteRenderer.sprite = blockState[hitCount];
        }
    }  
}
