using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {

    public static float GetJump()
    {
        return Input.GetAxis("Jump");
    }

}
