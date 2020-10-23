using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class Radar
    {
        Rectangle rectangle;

        public Radar(Vector2 pos, int width, int height)
        {
            rectangle = new Rectangle(pos.x, pos.y, width, height);
        }

        public void Draw()
        {
            DrawRectangleRec(rectangle, Color.WHITE);
            DrawRectangleRoundedLines(rectangle, .09f, 0, 5, Color.DARKGRAY);

            Vector2 startPoint = new Vector2(rectangle.x + (rectangle.width / 2), rectangle.y + (rectangle.height / 2));

            DrawLineEx(startPoint, startPoint + ((EnemyManager.nearestEnemy - Program.Center).Normalised() * 75), 5, Color.RED);
            DrawLineEx(startPoint, startPoint + ((AmmoManager.nearestAmmoDrop - Program.Center).Normalised() * 40), 5, Color.BLUE);
        }
    }
}
