using System.Linq;
using UnityEditor;

[CustomEditor(typeof(PlayerStateMachine))]
public class PlayerStateMachineEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerStateMachine fsm = (PlayerStateMachine)target;

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("State Machine");

        if (fsm.stateMachine == null) return;

        if (fsm.stateMachine.CurrentBase != null)
        {
            EditorGUILayout.LabelField("Current State: ", fsm.stateMachine.CurrentBase.ToString());
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States");

        if (showFoldout == true)
        {
            if (fsm.stateMachine.dictionaryState != null)
            {
                var keys = fsm.stateMachine.dictionaryState.Keys.ToArray();
                var values = fsm.stateMachine.dictionaryState.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format($"{keys[i]} <-:-> {values[i]}"));
                }
            }
        }

        if (UnityEngine.Application.isPlaying) Repaint();
    }
}