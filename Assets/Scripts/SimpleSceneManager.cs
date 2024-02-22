using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SimpleSceneManager : MonoBehaviour
{
    #if UNITY_EDITOR
    //assignments show or not
    [SerializeField] private bool _assignMentsShow = false;
    #endif

    [SerializeField] private Image cell00,cell01,cell02,cell10,cell11,cell12,cell20,cell21,cell22;
    public int stepCounter = 0;
    [SerializeField] private CellManager cellManager;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(stepCounter > 4){
            Check_Winer();
        }
    }

    void Check_Winer(){

        //horizontall check
        if(cell00.sprite != null && cell00.sprite == cell01.sprite && cell01.sprite == cell02.sprite){
            Set_results();
            SceneManager.LoadScene("ResultSeane");
        }
        else if(cell10.sprite != null && cell10.sprite == cell11.sprite && cell11.sprite == cell12.sprite){
            Set_results();
            SceneManager.LoadScene("ResultSeane");
        }
        else if(cell20.sprite != null && cell20.sprite == cell21.sprite && cell21.sprite == cell22.sprite){
            Set_results();
            SceneManager.LoadScene("ResultSeane");
        }

        //Vertical Check
        else if(cell00.sprite != null && cell00.sprite == cell10.sprite && cell10.sprite == cell20.sprite){
            Set_results();
            SceneManager.LoadScene("ResultSeane");
        }
        else if(cell01.sprite != null && cell01.sprite == cell11.sprite && cell11.sprite == cell21.sprite){
            Set_results();
            SceneManager.LoadScene("ResultSeane");
        }
        else if(cell02.sprite != null && cell02.sprite == cell12.sprite && cell12.sprite == cell22.sprite){
            Set_results();
            SceneManager.LoadScene("ResultSeane");
        }

        //cross Check
        else if(cell00.sprite != null && cell00.sprite == cell11.sprite && cell11.sprite == cell22.sprite){
            Set_results();
            SceneManager.LoadScene("ResultSeane");
        }
        else if(cell20.sprite != null && cell20.sprite == cell11.sprite && cell11.sprite == cell02.sprite){
            Set_results();
            SceneManager.LoadScene("ResultSeane");
        }

        //draw check
        else if(stepCounter == 9){
            Result.draw = true;
            SceneManager.LoadScene("ResultSeane");
        }
    }

    private void Set_results(){
        if(cellManager.currenTurn == CellManager.Turn.cross){
            Result.winer = "O";
            Result.draw = false;
        }
        else if(cellManager.currenTurn == CellManager.Turn.circle){
            Result.winer = "X";
            Result.draw = false;
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(SceneManager))]
    private class SimpleSceneManagerController: Editor{
        public override void OnInspectorGUI()
        {
            SimpleSceneManager simpleSceneManager = (SimpleSceneManager)target;

            serializedObject.Update();
            
            //make assign value in foldout
            simpleSceneManager._assignMentsShow = EditorGUILayout.Foldout(simpleSceneManager._assignMentsShow,"Assignments");
            if(simpleSceneManager._assignMentsShow){
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(simpleSceneManager.cell00)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(simpleSceneManager.cell01)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(simpleSceneManager.cell02)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(simpleSceneManager.cell10)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(simpleSceneManager.cell11)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(simpleSceneManager.cell12)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(simpleSceneManager.cell20)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(simpleSceneManager.cell21)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(simpleSceneManager.cell22)));

                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(simpleSceneManager.cellManager)));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
