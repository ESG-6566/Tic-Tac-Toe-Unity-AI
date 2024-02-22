using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AIBFS : MonoBehaviour
{
    #if UNITY_EDITOR
    //assignments show or not
    [SerializeField] private bool _assignMentsShow = false;
    #endif

    [SerializeField] AIButton aiButton;
    [SerializeField] private Button cell00,cell01,cell02,
                                    cell10,cell11,cell12,
                                    cell20,cell21,cell22;
    private List<List<Button>> board;
    [SerializeField] private CellManager cellManager;
    [SerializeField] private SimpleSceneManager simpleSceneManager ;

    void Awake()
    {
        board = new List<List<Button>>{
            new List<Button> {cell00,cell01,cell02},
            new List<Button> {cell10,cell11,cell12},
            new List<Button> {cell20,cell21,cell22}

        };
    }

    void FixedUpdate()
    {
        if(AIButton.aiEnabel == AIButton.AIEnabel.On && simpleSceneManager.stepCounter < 9){
            if(cellManager.currenTurn == CellManager.Turn.circle){
                (int i, int j) aiMove = GetAiMove();
                board[aiMove.i][aiMove.j].onClick.Invoke();
            }
        }
    }

    static bool IsBoardFull(List<List<string>> tableStatus)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (tableStatus[i][j] == "")
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool CheckWinner(List<List<string>> tableStatus, string player)
    {
        for (int i = 0; i < 3; i++)
        {
            if ((tableStatus[i][0] == player && tableStatus[i][1] == player && tableStatus[i][2] == player) ||
                (tableStatus[0][i] == player && tableStatus[1][i] == player && tableStatus[2][i] == player))
            {
                return true;
            }
        }

        if ((tableStatus[0][0] == player && tableStatus[1][1] == player && tableStatus[2][2] == player) ||
            (tableStatus[0][2] == player && tableStatus[1][1] == player && tableStatus[2][0] == player))
        {
            return true;
        }

        return false;
    }

    private (int, int) GetAiMove()
    {
        List<List<string>> boardStatus = cellManager.tableStatus;

        Queue<(int, int)> queue = new Queue<(int, int)>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (boardStatus[i][j] == "")
                {
                    queue.Enqueue((i, j));
                }
            }
        }

        while (queue.Count > 0)
        {
            (int row, int col) = queue.Dequeue();
            boardStatus[row][col] = "O";
            if (CheckWinner(boardStatus, "O"))
            {
                boardStatus[row][col] = "";
                return (row, col);
            }
            boardStatus[row][col] = "";
        }

        // If no winning move is found, make a random move
        List<(int, int)> emptyCells = new List<(int, int)>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (boardStatus[i][j] == "")
                {
                    emptyCells.Add((i, j));
                }
            }
        }

        int random = UnityEngine.Random.Range(0, emptyCells.Count);
        return emptyCells[random];
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(AIBFS))]
    private class AIBFSController: Editor{
        public override void OnInspectorGUI()
        {
            AIBFS aibfs = (AIBFS)target;

            serializedObject.Update();
            
            //make assign value in foldout
            aibfs._assignMentsShow = EditorGUILayout.Foldout(aibfs._assignMentsShow,"Assignments");
            if(aibfs._assignMentsShow){
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(aibfs.cell00)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(aibfs.cell01)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(aibfs.cell02)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(aibfs.cell10)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(aibfs.cell11)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(aibfs.cell12)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(aibfs.cell20)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(aibfs.cell21)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(aibfs.cell22)));

                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(aibfs.cellManager)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(aibfs.aiButton)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(aibfs.simpleSceneManager)));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
