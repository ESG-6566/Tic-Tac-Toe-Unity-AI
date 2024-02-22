using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MinimaxAI : MonoBehaviour
{
    #if UNITY_EDITOR
    //assignments show or not
    [SerializeField] private bool _assignMentsShow = false;
    #endif

    [SerializeField] AIButton aiButton;

    [SerializeField] private Button cell00,cell01,cell02,
                                    cell10,cell11,cell12,
                                    cell20,cell21,cell22;
    private List<List<Button>> tabel;

    [SerializeField] private CellManager cellManager;

    void Awake()
    {
        tabel = new List<List<Button>>{
            new List<Button> {cell00,cell01,cell02},
            new List<Button> {cell10,cell11,cell12},
            new List<Button> {cell20,cell21,cell22}

        };
    }

    void FixedUpdate()
    {
        if(AIButton.aiEnabel == AIButton.AIEnabel.On){
            if(cellManager.currenTurn == CellManager.Turn.circle){
                (int i, int j) aiMove = GetAiMove();
                tabel[aiMove.i][aiMove.j].onClick.Invoke();
            }
        }
    }

    private (int i, int j) GetAiMove()
    {
        List<List<string>> boardStatus = cellManager.tableStatus;

        int bestScore = int.MinValue;
        (int, int) bestMove = (0, 0);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (boardStatus[i][j] == "")
                {
                    boardStatus[i][j] = "O";
                    int score = Minimax(boardStatus, false);
                    boardStatus[i][j] = "";

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = (i, j);
                    }
                }
            }
        }

        return bestMove;
    }

    private int Minimax(List<List<string>> tableStatus, bool isMaximizing)
    {
        if (CheckWinner(tableStatus, "X"))
        {
            return -1;
        }
        else if (CheckWinner(tableStatus, "O"))
        {
            return 1;
        }
        else if (IsTabelFull(tableStatus))
        {
            return 0;
        }

        if (isMaximizing)
        {
            int maxEval = int.MinValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tableStatus[i][j] == "")
                    {
                        tableStatus[i][j] = "O";
                        int eval = Minimax(tableStatus, false);
                        tableStatus[i][j] = "";
                        maxEval = Math.Max(maxEval, eval);
                    }
                }
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tableStatus[i][j] == "")
                    {
                        tableStatus[i][j] = "X";
                        int eval = Minimax(tableStatus, true);
                        tableStatus[i][j] = "";
                        minEval = Math.Min(minEval, eval);
                    }
                }
            }
            return minEval;
        }
    }

    static bool IsTabelFull(List<List<string>> tableStatus)
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

    #if UNITY_EDITOR
    [CustomEditor(typeof(MinimaxAI))]
    private class MinimaxAIController: Editor{
        public override void OnInspectorGUI()
        {
            MinimaxAI minimaxAI = (MinimaxAI)target;

            serializedObject.Update();
            
            //make assign value in foldout
            minimaxAI._assignMentsShow = EditorGUILayout.Foldout(minimaxAI._assignMentsShow,"Assignments");
            if(minimaxAI._assignMentsShow){
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(minimaxAI.cell00)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(minimaxAI.cell01)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(minimaxAI.cell02)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(minimaxAI.cell10)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(minimaxAI.cell11)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(minimaxAI.cell12)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(minimaxAI.cell20)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(minimaxAI.cell21)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(minimaxAI.cell22)));

                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(minimaxAI.cellManager)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(minimaxAI.aiButton)));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
