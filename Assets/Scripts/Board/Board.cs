using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Board
{
    public class Board : MonoBehaviour
    {
        // Just FYI. Not the ideal way to access these. But the easiest way for now.
        [SerializeField] private Player player;
        [SerializeField] private Tile start;

        // Player tile index. Static so it gets kept across scenes
        private static int _playerTileI;

        private State _state = new Waiting();
        private readonly List<Tile> _tiles = new();

        private void OnEnable()
        {
            // Bind space bar action
            InputManager.Instance.OnSpace += player.StopRoll;
        }

        private void OnDisable()
        {
            // Unbind space bar action
            InputManager.Instance.OnSpace -= player.StopRoll;
        }

        private void Awake()
        {
            // Convert the linked list of the tiles into an array. Not cached/static for simplicity
            _tiles.Add(start);
            for (var tile = start.next; tile != start; tile = tile.next)
            {
                _tiles.Add(tile);
            }
        }

        private void Start()
        {
            // Set player position to the right tile, in case we are switching to this scene
            player.transform.position = _tiles[_playerTileI].transform.position;
            // Using anonymous function as callback.
            // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/delegate-operator 
            player.StartRoll(roll => _state = new Walking { Moves = roll });
        }

        private void Update()
        {
            // Ignoring the Waiting state here
            switch (_state)
            {
                case Walking w:
                    Walk(w);
                    break;
            }
        }

        /// <summary>
        /// Walk the player closer to next tile.
        /// </summary>
        /// <param name="w">Stores the amount of moves left for the player.</param>
        private void Walk(Walking w)
        {
            // Move player closer to next tile
            var next = (_playerTileI + 1) % _tiles.Count;
            if (!player.Move(_tiles[next].transform.position)) return;

            // Player arrived at tile
            _playerTileI = next;
            w.Moves -= 1;
            if (w.Moves != 0) return;

            // Player is out of moves
            _state = new Waiting();
            SceneManager.LoadScene("MiniGameScene");
        }

        // Basic implementation of a tagged unions
        private abstract class State
        {
        }

        private class Waiting : State
        {
        }

        private class Walking : State
        {
            public int Moves { get; set; }
        }
    }
}