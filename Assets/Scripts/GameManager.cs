using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { None, Play, Win, Lose }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState GameState { get; set; }
    public PlayerCharacter player;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
}
