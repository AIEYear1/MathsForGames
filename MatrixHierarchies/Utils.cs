using Raylib_cs;
using System;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    static class Utils
    {
        public static float Lerp(float start, float end, float increment)
        {
            float toReturn = start;
            increment = MathF.Max(increment, 0);
            increment = MathF.Min(increment, 1);

            toReturn += (end - start) * increment;

            return toReturn;
        }
        /// <summary>
        /// To Return Values for GetAxis()
        /// </summary>
        private static float toReturnHorizontal = 0, toReturnVertical = 0, toReturnTurret = 0;
        /// <summary>
        /// Gets input axes for player movement
        /// </summary>
        /// <param name="axis">input axis to get</param>
        /// <param name="sensitivity">how easily the player moves up to speed</param>
        /// <returns>Returns a float between 1 and -1 for the specified axis</returns>
        public static float GetAxis(string axis, float sensitivity = 3)
        {
            //How small the number has to be to not be registered
            float dead = .001f;
            //what the axis will try and reach
            float target = 0;

            switch (axis)
            {
                //1st case "Vertical", returns between 1 (W key) and -1 (S key)
                case "Vertical":
                    if (IsKeyDown(KeyboardKey.KEY_W))
                        target = 1;
                    if (IsKeyDown(KeyboardKey.KEY_S))
                        target = -1;

                    toReturnVertical = Utils.Lerp(toReturnVertical, target, sensitivity * GetFrameTime());
                    return (MathF.Abs(toReturnVertical) < dead) ? 0f : toReturnVertical;
                //2nd case "Horizontal", returns between 1 (D key) and -1 (A key)
                case "Horizontal":
                    if (IsKeyDown(KeyboardKey.KEY_D))
                        target = 1;
                    if (IsKeyDown(KeyboardKey.KEY_A))
                        target = -1;

                    toReturnHorizontal = Utils.Lerp(toReturnHorizontal, target, sensitivity * GetFrameTime());
                    return (MathF.Abs(toReturnHorizontal) < dead) ? 0f : toReturnHorizontal;
                //3rd case "Turret", returns between 1 (E key) and -1 (Q key)
                case "Turret":
                    if (IsKeyDown(KeyboardKey.KEY_E))
                        target = 1;
                    if (IsKeyDown(KeyboardKey.KEY_Q))
                        target = -1;

                    toReturnTurret = Utils.Lerp(toReturnTurret, target, sensitivity * GetFrameTime());
                    return (MathF.Abs(toReturnTurret) < dead) ? 0f : toReturnTurret;
                //Overflow
                default:
                    return 0;
            }
        }
    }
}
