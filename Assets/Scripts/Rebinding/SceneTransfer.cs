using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    public void TransferToScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
