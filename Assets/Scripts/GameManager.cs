using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    enum gameState
    {
        playing,
        dialogue,
        cutscene,
        paused,
    }

    gameState _currentGameState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentGameState = gameState.playing;
    }

    public bool GameIsPlaying()
    {
        return _currentGameState == gameState.playing;
    }

    public bool GameIsDialogue()
    {
        return _currentGameState == gameState.dialogue;
    }

    public void SetGameIsDialogue()
    {
        _currentGameState = gameState.dialogue;
    }
    public void SetGameIsPlaying()
    {
        _currentGameState = gameState.playing;
    }
}
