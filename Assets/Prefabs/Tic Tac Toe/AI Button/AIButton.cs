using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Animations;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AIButton : MonoBehaviour
{
    #if UNITY_EDITOR
    //assignments show or not
    [SerializeField] private bool _assignMentsShow = false;
    #endif

    [SerializeField] private TextMeshProUGUI statusTMP;
    [SerializeField] public static AIEnabel aiEnabel = AIEnabel.Off;
    private Image image;
    private Animator animator;

    void Awake()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();

        switch(aiEnabel){
            case AIEnabel.On:
                animator.Play("Toggle on", -1, 1.0f);
                
                break;
            case AIEnabel.Off:
                animator.Play("Toggle off", -1, 1.0f);
                break;
        }
    }

    public enum AIEnabel{
        On, Off
    }

    public void AISwitchOnOff(){
        switch(aiEnabel){
            case AIEnabel.On:
                aiEnabel = AIEnabel.Off;
                animator.Play("Toggle off");
                break;
            case AIEnabel.Off:
                aiEnabel = AIEnabel.On;
                animator.Play("Toggle on");
                break;
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(AIButton))]
    private class AIButtonController: Editor{
        public override void OnInspectorGUI()
        {
            AIButton aiButton = (AIButton)target;

            serializedObject.Update();
            
            //make assign value in foldout
            aiButton._assignMentsShow = EditorGUILayout.Foldout(aiButton._assignMentsShow,"Assignments");
            if(aiButton._assignMentsShow){

                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(aiButton.statusTMP)));

                EditorGUILayout.Space();
            }
            //EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(AIButton.aiEnabel)));

            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
