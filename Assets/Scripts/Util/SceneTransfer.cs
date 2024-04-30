using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    public void TransferToScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
