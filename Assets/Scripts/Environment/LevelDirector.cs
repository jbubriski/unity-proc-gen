using UnityEngine;
using UnityEngine.UI;

public class LevelDirector : LevelDirectorBase
{
    private static LevelDirector _this;

    private Transform _floor;
    private Transform _playerSpawnPoint;

    public Transform Exit;

    public Transform[] Tiles;
    public Transform PlayerSpawnPointPrefab;
    public Transform PlayerPrefab;

    public Text FloorValueUiText;

    public int GridSize = 20;
    public int TileSize = 4;

    public int Floor = 1;

    public static LevelDirector Instance
    {
        get
        {
            if (_this == null)
            {
                _this = FindObjectOfType<LevelDirector>();
            }

            return _this;
        }
    }

    public void Start()
    {
        Destroy(transform.FindChild("Placeholder").gameObject);

        _floor = transform.FindChild("Floor");

        ResetAndSetup();
    }

    public void Update()
    {

    }

    public void ResetAndSetup()
    {
        RemovePlayer();
        ResetMap();
        SetupMap();

        SpawnPlayer();
    }

    public void RemovePlayer()
    {
        var player = FindObjectOfType<PlayerHealth>();

        if (player != null)
            Destroy(player.gameObject);
    }

    public void ResetMap()
    {
        ClearObjects();

        Floor = 1;
        UpdateFloor();
    }

    public override void NextFloor()
    {
        ClearObjects(); 
        SetupMap();

        Floor++;
        UpdateFloor();
    }

    private void ClearObjects()
    {
        ClearChildren(_floor);
    }

    public void SetupMap()
    {
        transform.Rotate(0, -45, 0);

        var gridWidth = GridSize * TileSize;
        var spawnedExit = false;

        for (var i = 0; i < GridSize; i++)
        {
            for (var j = 0; j < GridSize; j++)
            {
                if (_playerSpawnPoint == null && Random.value * 100 > 90 && i > 2) // Exit - 10%
                {
                    _playerSpawnPoint = (Transform)Instantiate(PlayerSpawnPointPrefab, transform);
                    _playerSpawnPoint.position = new Vector3(i * TileSize - gridWidth / 2, 1, j * TileSize - gridWidth / 2);

                    var newTile = (Transform)Instantiate(Tiles.RandomElement(), _floor);
                    newTile.position = new Vector3(i * TileSize - gridWidth / 2, 0, j * TileSize - gridWidth / 2);
                }
                else if (!spawnedExit && Random.value * 100 > 90) // Exit - 10%
                {
                    var exit = (Transform)Instantiate(Exit, _floor);
                    exit.position = new Vector3(i * TileSize - gridWidth / 2, 0, j * TileSize - gridWidth / 2);

                    spawnedExit = true;
                }
                else
                {
                    var newTile = (Transform)Instantiate(Tiles.RandomElement(), _floor);
                    newTile.position = new Vector3(i * TileSize - gridWidth / 2, 0, j * TileSize - gridWidth / 2);
                }
            }
        }

        transform.Rotate(0, 45, 0);
    }

    public Transform SpawnPlayer()
    {
        var player = (Transform)Instantiate(PlayerPrefab, transform);

        if (player != null)
        {
            player.position = _playerSpawnPoint.position;
        }

        return player;
    }

    private void UpdateFloor()
    {
        FloorValueUiText.text = Floor.ToString();
    }

    private void ClearChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            var childTransform = parent.GetChild(i);

            Destroy(childTransform.gameObject);
        }
    }
}
