using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneDropdown : MonoBehaviour
{
    public TMP_Dropdown dd;

    public MainMenu menu;

    private void Start()
    {
        dd = GetComponent<TMP_Dropdown>();
    }

    public void SetCurrentScene()
    {
        menu.ActiveScene = dd.options[dd.value].text;

        menu.ActiveSceneNumber = dd.value;
    }
}
