using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.Data
{
    [CreateAssetMenu(fileName = "New QuestionData", menuName = "Configs/QuestionData")]
    public class QuestionDataSO : ScriptableObject
    {
        [field: SerializeField] public List<QuestionGroup> Questions { get; set; }
        [field: SerializeField] public int MoneyForComplete { get; set; }
    }

    [Serializable]
    public class QuestionGroup
    {
        [field: SerializeField] public string Question { get; set; }
        [field: SerializeField] public int AnswerIndex { get; set; }
        [field: SerializeField] public string FirstAnswer { get; set; }
        [field: SerializeField] public string SecondAnswer { get; set; }
        [field: SerializeField] public string ThirdAnswer { get; set; }
    }
}