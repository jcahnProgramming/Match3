using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneDropdown : MonoBehaviour
{
    public TMP_Dropdown dd;

    public void SetCurrentScene()
    {
        PlayerPrefs.SetString("SceneToPlay", dd.options[dd.value].text);
        //MainMenu.Instance.ActiveScene = dd.options[dd.value].text;
    }
}
