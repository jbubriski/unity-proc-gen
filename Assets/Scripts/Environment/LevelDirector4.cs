using UnityEngine;
using UnityEngine.UI;

public class LevelDirector4 : LevelDirectorBase
{
    private static LevelDirector4 _this;

    private Transform _floor;
    private Transform _traps;
    private Transform _decorations;
    private Transform _enemies;
    private Transform _extras;
    private Transform _playerSpawnPoint;

    public Transform Exit;

    public Transform[] Tiles;
    public Transform[] Traps;
    public Transform[] Decorations;
    public Transform[] Enemies;
    public Transform PlayerSpawnPointPrefab;
    public Transform PlayerPrefab;

    public Text FloorValueUiText;

    public int GridSize = 20;
    public int TileSize = 4;

    public int Floor = 1;

    public static LevelDirector4 Instance
    {
        get
        {
            if (_this == null)
            {
                _this = FindObjectOfType<LevelDirector4>();
            }

            return _this;
        }
    }

    public void Start()
    {
        Destroy(transform.FindChild("Placeholder").gameObject);

        _floor = transform.FindChild("Floor");
        _traps = transform.FindChild("Traps");
        _decorations = transform.FindChild("Decorations");
        _enemies = transform.FindChild("Enemies");
        _extras = transform.FindChild("Extras");

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
        ClearChildren(_traps);
        ClearChildren(_decorations);
        ClearChildren(_enemies);
        ClearChildren(_extras);
    }

    public bool[][] CreateCircle(int radius, int gridSize)
    {
        var circleGrid = new bool[gridSize][];
        
        for (var i = 0; i < gridSize; i++)
        {
            circleGrid[i] = new bool[gridSize];
        }
        
        var offset = 10;

        var x = radius;
        var y = 0;
        var err = 0;

        while (x >= y)
        {
            circleGrid[offset + x][offset + y] = true;
            circleGrid[offset + y][offset + x] = true;
            circleGrid[offset - y][offset + x] = true;
            circleGrid[offset - x][offset + y] = true;
            circleGrid[offset - x][offset - y] = true;
            circleGrid[offset - y][offset - x] = true;
            circleGrid[offset + y][offset - x] = true;
            circleGrid[offset + x][offset - y] = true;

            y += 1;
            err += 1 + 2 * y;
            if (2 * (err - x) + 1 > 0)
            {
                x -= 1;
                err += 1 - 2 * x;
            }
        }

        for (var i = 0; i < circleGrid.Length; i++)
        {
            var edgeFound = false;
            var startOfEdge = 0;

            for (var j = 0; j < circleGrid[i].Length; j++)
            {
                if (!edgeFound && circleGrid[i][j])
                {
                    edgeFound = true;
                    startOfEdge = j;
                }
                else if (edgeFound && j < circleGrid[i].Length - startOfEdge)
                {
                    circleGrid[i][j] = true;
                }
            }
        }

        return circleGrid;
    }

    public void SetupMap()
    {
        transform.Rotate(0, -45, 0);

        var gridWidth = GridSize * TileSize;
        var spawnedExit = false;

        var perlinOffset = new Vector2(Random.Range(0, 100), Random.Range(0, 100));

        var circle = CreateCircle(9, GridSize);

        for (var i = 0; i < GridSize; i++)
        {
            for (var j = 0; j < GridSize; j++)
            {
                if (!circle[i][j])
                {
                    continue;
                }

                var perlin = Mathf.PerlinNoise((float)i / 10 + perlinOffset.x, (float)j / 10 + perlinOffset.y) * 100f;
                Debug.Log(perlin);

                if (perlin > 20)
                {
                    var newTile = (Transform)Instantiate(Tiles.RandomElement(), _floor);
                    newTile.position = new Vector3(i * TileSize - gridWidth / 2, 0, j * TileSize - gridWidth / 2);

                    if (_playerSpawnPoint == null && Random.value * 100 > 90 && i > 2) // Exit - 10%
                    {
                        _playerSpawnPoint = (Transform)Instantiate(PlayerSpawnPointPrefab, transform);
                        _playerSpawnPoint.position = new Vector3(i * TileSize - gridWidth / 2, 1, j * TileSize - gridWidth / 2);
                    }
                    else if (!spawnedExit && Random.value * 100 > 90) // Exit - 10%
                    {
                        Destroy(newTile.gameObject);

                        var exit = (Transform)Instantiate(Exit, _floor);
                        exit.position = new Vector3(i * TileSize - gridWidth / 2, 0, j * TileSize - gridWidth / 2);

                        spawnedExit = true;
                    }
                    else if (Random.value * 100 > 95) // Enemy
                    {
                        var newEnemy = (Transform)Instantiate(Enemies.RandomElement(), _enemies);
                        newEnemy.position = new Vector3(i * TileSize - gridWidth / 2 + 2, 2, j * TileSize - gridWidth / 2 + 2);
                    }
                    else if (Random.value * 100 > 95) // Traps
                    {
                        var newTrap = (Transform)Instantiate(Traps.RandomElement(), _traps);
                        newTrap.position = new Vector3(i * TileSize - gridWidth / 2, 0, j * TileSize - gridWidth / 2);
                    }
                    else if (Random.value * 100 > 95) // Decorations
                    {
                        var newDecoration = (Transform)Instantiate(Decorations.RandomElement(), _decorations);
                        newDecoration.position = new Vector3(i * TileSize - gridWidth / 2, 0, j * TileSize - gridWidth / 2);
                    }
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
