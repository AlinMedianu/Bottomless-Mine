using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the file where the interfeces are held
public interface IBlink
{
    Color OriginalColor { get; set; }
    Color InvertedColor { get; set; }
    Color CurrentColor { get; set; }
    IEnumerator BlinkColor();
}
