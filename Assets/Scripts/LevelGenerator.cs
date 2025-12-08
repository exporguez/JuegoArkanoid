using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject[] blockPrefabs; // Array de prefabs de bloques a generar
    public GameObject[] blockPositions; // Array de posiciones donde se generarán los bloques

    public GameObject levelGenerator;
    private bool gameStarted = false;

    public void GenerateBlocksRandomly()// Generar bloques en posiciones aleatorias
    {
        if (blockPrefabs == null || blockPrefabs.Length == 0)// Verificar si el array de prefabs está vacío
        {
            Debug.LogError("No hay prefabs de bloques asignados!");
            return;
        }

        if (blockPositions == null || blockPositions.Length == 0)// Verificar si el array de posiciones está vacío
        {
            Debug.LogError("No hay posiciones de bloques asignadas!");
            return;
        }

        for (int i = 0; i < blockPositions.Length; i++)
        {
            GameObject currentPosition = blockPositions[i];

            if (currentPosition == null)
            {
                Debug.LogWarning($"La posición en el índice {i} es nula. Se omitirá.");
                continue;
            }
            
            Vector3 worldPosition = currentPosition.transform.position;// Obtener la posición en el mundo

            int randomePrefabIndex = Random.Range(0, blockPrefabs.Length);
            GameObject prefabElegido = blockPrefabs[randomePrefabIndex];

            GameObject newBlock = Instantiate(prefabElegido, worldPosition, Quaternion.identity);

            newBlock.transform.SetParent(levelGenerator.transform, true);

            currentPosition.SetActive(false); // Destruir la posición después de generar el bloque
        }
    }

    public void StartGame()// Iniciar el juego y generar los bloques
    {
        if (levelGenerator != null)
        {
            levelGenerator.SetActive(true);
        }
        if (!gameStarted)
        {
            GenerateBlocksRandomly();
            gameStarted = true;
        }

        Debug.Log("Juego Iniciado");
    }

    public void BackGame()// Salir del juego y desactivar los bloques generados
    {
        levelGenerator.SetActive(false);

        for(int i = levelGenerator.transform.childCount - 1;i >= 0; i--)
        {
            levelGenerator.transform.GetChild(i).gameObject.SetActive(false);// Desactivar cada bloque generado
        }
        
        gameStarted = false;

        Debug.Log("Se salio del juego");
    }
}
