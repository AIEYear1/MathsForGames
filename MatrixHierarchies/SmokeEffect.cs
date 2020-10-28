using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class SmokeEffect : SceneObject
    {
        BoxCollider renderCollider;

        Vector2 smokeOffset;
        Texture2D curTexture, smokeTexture1, smokeTexture2, smokeTexture3;

        Timer timerHell;

        Color curColor, fireColor = new Color(226, 88, 34, 220), smokeColor = new Color(94, 79, 68, 0);
        float curScale = .5f;
        float curRot = 0;

        public SmokeEffect(Vector2 position, float lifeTime)
        {
            timerHell = new Timer(lifeTime);

            smokeTexture1 = PreLoadedTextures.smokeTexture1;
            smokeTexture2 = PreLoadedTextures.smokeTexture2;
            smokeTexture3 = PreLoadedTextures.smokeTexture3;
            curTexture = smokeTexture1;

            smokeOffset = new Vector2(smokeTexture1.width / 2, smokeTexture1.height / 2);
            smokeOffset *= curScale;

            SetPosition(position.x, position.y);

            renderCollider = new BoxCollider(Position, smokeTexture3.width, smokeTexture3.height, 0);
        }

        public override void OnUpdate(float deltaTime)
        {
            if (!timerHell.Check(false))
            {
                curColor = ColorRGB.Lerp(fireColor, smokeColor, timerHell.PercentComplete);
                curScale = Utils.Lerp(0.2f, 1, timerHell.PercentComplete);
                curRot = Utils.Lerp(0, 90, timerHell.PercentComplete);

                if (timerHell.PercentComplete >= 2f / 3f)
                {
                    curTexture = smokeTexture3;
                    smokeOffset.x = smokeTexture3.width / 2;
                    smokeOffset.y = smokeTexture3.height / 2;
                }
                else if (timerHell.PercentComplete >= 1f / 3f)
                {
                    curTexture = smokeTexture2;
                    smokeOffset.x = smokeTexture2.width / 2;
                    smokeOffset.y = smokeTexture2.height / 2;
                }
                else
                {
                    smokeOffset.x = smokeTexture1.width / 2;
                    smokeOffset.y = smokeTexture1.height / 2;
                }
                smokeOffset *= curScale;

                base.OnUpdate(deltaTime);
                return;
            }

            // Death Zone
            SmokeManager.RemoveSmoke(this);
        }

        public override void OnDraw()
        {
            if (Collider.Collision(renderCollider, (BoxCollider)Program.ScreenSpace))
            {
                DrawTextureEx(curTexture, Position - smokeOffset, curRot, curScale, curColor);
            }
        }
    }
}
