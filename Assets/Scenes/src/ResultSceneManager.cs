using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class ResultSceneManager : MonoBehaviour
{
    #if UNITY_EDITOR
    //assignments show or not
    [SerializeField] private bool _assignMentsShow = false;
    #endif

    [SerializeField] private Image WinerIcon;
    [SerializeField] private Sprite cross,circle,equal;
    [SerializeField] private Color crossColor,circleColor,equalColor;
    [SerializeField] private TextMeshProUGUI resultText;
    // Start is called before the first frame update
    void Awake()
    {
        if(Result.draw == true){
            resultText.text = "Equal";
            WinerIcon.sprite = equal;
            WinerIcon.color = equalColor;
        }
        else if(Result.winer == "X"){
            WinerIcon.sprite = cross;
            WinerIcon.color = crossColor;
        }
        else if(Result.winer == "O"){
            WinerIcon.sprite = circle;
            WinerIcon.color = circleColor;
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(ResultSceneManager))]
    private class ResultSceneManagerController: Editor{
        public override void OnInspectorGUI()
        {
            ResultSceneManager resultSceneManager = (ResultSceneManager)target;

            serializedObject.Update();
            
            //make assign value in foldout
            resultSceneManager._assignMentsShow = EditorGUILayout.Foldout(resultSceneManager._assignMentsShow,"Assignments");
            if(resultSceneManager._assignMentsShow){
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(resultSceneManager.WinerIcon)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(resultSceneManager.resultText)));
                EditorGUILayout.Space();
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(resultSceneManager.cross)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(resultSceneManager.circle)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(resultSceneManager.equal)));

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(resultSceneManager.crossColor)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(resultSceneManager.circleColor)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(resultSceneManager.equalColor)));
            

            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
