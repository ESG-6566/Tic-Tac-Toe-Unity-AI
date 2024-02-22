using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Cell : MonoBehaviour
{
    #if UNITY_EDITOR
    //assignments show or not
    [SerializeField] private bool _assignMentsShow = false;
    #endif

    public int row,column;

    #if UNITY_EDITOR
    [CustomEditor(typeof(Cell))]
    private class CellController: Editor{
        public override void OnInspectorGUI()
        {
            Cell cell = (Cell)target;

            serializedObject.Update();
            
            // //make assign value in foldout
            // cell._assignMentsShow = EditorGUILayout.Foldout(cell._assignMentsShow,"Assignments");
            // if(cell._assignMentsShow){
                
            //     EditorGUILayout.Space();
            // }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Row and Column", GUILayout.Width(110));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(cell.row)), GUIContent.none, GUILayout.Width(50));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(cell.column)), GUIContent.none, GUILayout.Width(50));
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
