using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    
    /// <summary>
    /// Pauses the game. Coroutines are scaled of time scale, so it pauses them.
    /// </summary>
    public void PauseGame() {
        Time.timeScale = 0f;
    }
    
    /// <summary>
    /// Unppauses the game. Coroutines are scaled of time scale, so it resumes them.
    /// </summary>
    public void UnpauseGame() {
        Time.timeScale = 1f;
    }

    public void ReloadCurrentScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

/// <summary>
/// Info for the level phases. How much it takes for it to end and what happens when it ends. Fully customizable on the
/// editor.
/// </summary>
[Serializable]
public class LevelPhaseInfo {
    public float duration;

    public UnityEvent onEndedEvent;
}
