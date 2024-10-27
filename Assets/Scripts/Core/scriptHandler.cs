using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptHandler : MonoBehaviour
{
    [System.Serializable]
    public class StoryLine
    {
        public string storyText; // Individual story text
    }

    [System.Serializable]
    public class StoryElement
    {
        public List<StoryLine> storyLines; 
    }

    [Header("Story Elements")]
    public List<StoryElement> storyElements; 

    [System.Serializable]
    public class HintLine
    {
        public string hintLine;
    }

    [Header("Hint Lines")]
    public List<HintLine> hintLines;
}
