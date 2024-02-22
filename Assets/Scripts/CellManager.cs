using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CellManager : MonoBehaviour
{
    #if UNITY_EDITOR
    //assignments show or not
    [SerializeField] private bool _assignMentsShow = false;
    #endif

    [SerializeField] private Sprite cross,circle;
    [SerializeField] private Color crossColor,circleColor;
    [SerializeField] private SimpleSceneManager simpleSceneManager;
    [SerializeField] private Image turn;
    public List<List<string>> tableStatus = new List<List<string>>{
        new List<string> {"","",""},
        new List<string> {"","",""},
        new List<string> {"","",""}

    };

    public enum Turn{
        cross,circle
    }
    public Turn currenTurn = Turn.cross;

    public void Cell_clicked(Image image){

        Cell cell = image.gameObject.GetComponent<Cell>() ;
        
        switch(currenTurn){
            case Turn.cross:
                tableStatus[cell.row][cell.column] = "X";
                image.sprite = cross;
                image.color = crossColor;
                image.gameObject.GetComponent<Button>().enabled = false;
                turn.sprite = circle;
                turn.color = circleColor;
                simpleSceneManager.stepCounter++;
                Switch_Turn();
                break;
            case Turn.circle:
                tableStatus[cell.row][cell.column] = "O";
                image.sprite = circle;
                image.color = circleColor;
                image.gameObject.GetComponent<Button>().enabled = false;
                turn.sprite = cross;
                turn.color = crossColor;
                simpleSceneManager.stepCounter++;
                Switch_Turn();
                break;
        }
    }

    private void Switch_Turn(){
        switch(currenTurn){
            case Turn.cross:
                currenTurn = Turn.circle;
                break;
            case Turn.circle:
                currenTurn = Turn.cross;
                break;
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(CellManager))]
    private class CellManagerController: Editor{
        public override void OnInspectorGUI()
        {
            CellManager cellManager = (CellManager)target;

            serializedObject.Update();
            
            //make assign value in foldout
            cellManager._assignMentsShow = EditorGUILayout.Foldout(cellManager._assignMentsShow,"Assignments");
            if(cellManager._assignMentsShow){
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(cellManager.simpleSceneManager)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(cellManager.turn)));
            }
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(cellManager.cross)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(cellManager.circle)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(cellManager.crossColor)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(cellManager.circleColor)));

            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
