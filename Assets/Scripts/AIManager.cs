using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TicTacToe{
    public class AIManager : MonoBehaviour
    {
        #if UNITY_EDITOR
        //assignments show or not
        [SerializeField] private bool _assignMentsShow = false;
        #endif
        
        [SerializeField] private GameObject MinimaxAIButton,BFSAIButton;
        [SerializeField] private static string selectedAI = "AI BFS Button";

        void Awake()
        {
            switch(selectedAI){
                case "AI MinimaxAI Button" :
                    MinimaxAIButton.GetComponent<MinimaxAI>().enabled = true;
                    MinimaxAIButton.GetComponent<Animator>().Play("Switch enabel", -1, 1.0f);

                    BFSAIButton.GetComponent<AIBFS>().enabled = false;
                    BFSAIButton.GetComponent<Animator>().Play("Switch disabel", -1, 1.0f);
                    break;
                case "AI BFS Button" :
                    BFSAIButton.GetComponent<AIBFS>().enabled = true;
                    BFSAIButton.GetComponent<Animator>().Play("Switch enabel", -1, 1.0f);

                    MinimaxAIButton.GetComponent<MinimaxAI>().enabled = false;
                    MinimaxAIButton.GetComponent<Animator>().Play("Switch disabel", -1, 1.0f);
                    break;
            }
        }

        public void AIStrategyClicked(GameObject Button){
            switch(Button.name){
                case "AI MinimaxAI Button" :
                    selectedAI = MinimaxAIButton.name;
                    MinimaxAIButton.GetComponent<MinimaxAI>().enabled = true;
                    MinimaxAIButton.GetComponent<Animator>().Play("Switch enabel");

                    BFSAIButton.GetComponent<AIBFS>().enabled = false;
                    BFSAIButton.GetComponent<Animator>().Play("Switch disabel");
                    break;
                case "AI BFS Button" :
                    selectedAI = BFSAIButton.name;
                    BFSAIButton.GetComponent<AIBFS>().enabled = true;
                    BFSAIButton.GetComponent<Animator>().Play("Switch enabel");

                    MinimaxAIButton.GetComponent<MinimaxAI>().enabled = false;
                    MinimaxAIButton.GetComponent<Animator>().Play("Switch disabel");
                    break;
            }
        } 

        //private void DisableOthers(GameObject.)

        #if UNITY_EDITOR
        [CustomEditor(typeof(AIManager))]
        private class AIManagerController: Editor{
            public override void OnInspectorGUI()
            {
                AIManager aiManager = (AIManager)target;

                serializedObject.Update();
                
                //make assign value in foldout
                aiManager._assignMentsShow = EditorGUILayout.Foldout(aiManager._assignMentsShow,"Assignments");
                if(aiManager._assignMentsShow){
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(aiManager.BFSAIButton)));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(aiManager.MinimaxAIButton)));

                    // EditorGUILayout.Space();
                }

                serializedObject.ApplyModifiedProperties();
            }
        }
        #endif
    }
    
}
