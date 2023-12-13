using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnClicked;
}
