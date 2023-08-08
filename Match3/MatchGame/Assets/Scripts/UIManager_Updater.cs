using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIManager_Updater : MonoBehaviour
{
    UIManager m_uiManager;

    [SerializeField]public SerializedProperty collectionGoalLayout;
    [SerializeField] string m_collectionGoal = "CollectionGoalLayout";


    // Start is called before the first frame update
    void Start()
    {
        m_uiManager = UIManager.Instance;

        SerializedObject obj = new UnityEditor.SerializedObject(m_uiManager);
        var o = obj.FindProperty(m_collectionGoal);
        o.objectReferenceValue = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
