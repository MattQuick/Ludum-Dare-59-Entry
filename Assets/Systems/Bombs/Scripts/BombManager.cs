using UnityEngine;

public class BombManager : MonoBehaviour
{
    [SerializeField] private TreeGenerator treeGenerator;
    [SerializeField] private GameObject bombPrefab;

    private int bombCount = 100;
    private BombObject[] bombPool;

    private int lastPlacedBombIndex = 0;
    [SerializeField] private int increaseEachWave = 5;

    private void Start()
    {
        GenerateBombs();
    }

    private void OnEnable()
    {
        GameEvents.ON_GAME_STATE_CHANGED += Handle_OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameEvents.ON_GAME_STATE_CHANGED -= Handle_OnGameStateChanged;
    }

    private void Handle_OnGameStateChanged(GameStateHolder.GameState _state)
    {
        if (_state == GameStateHolder.GameState.SEARCHING_FOR_SIGNAL)
        {
            RandomlyPlaceBombs();
            return;
        }

        if (_state != GameStateHolder.GameState.MAIN_MENU)
            return;

        ResetAllBombs();
    }

    private void ResetAllBombs()
    {
        lastPlacedBombIndex = 0;

        for (int i = 0; i < bombCount; i++)
        {
            bombPool[i].transform.position = Vector3.down * 100f;
            bombPool[i].DeActivate();
        }
    }

    private void GenerateBombs()
    {
        bombPool = new BombObject[bombCount];

        for (int i = 0; i < bombCount; i++)
        {
            bombPool[i] = GameObject.Instantiate(bombPrefab, transform).GetComponent<BombObject>();
            bombPool[i].transform.position = Vector3.down * 100f;
        }
    }

    public void RandomlyPlaceBombs()
    {
        if (lastPlacedBombIndex >= bombCount)
            return;

        for (int i = 0; i < increaseEachWave; i++)
        {
            bombPool[lastPlacedBombIndex].transform.position = treeGenerator.GetRandomPos();
            bombPool[lastPlacedBombIndex].EnableBomb();

            lastPlacedBombIndex++;
        }
    }
}
