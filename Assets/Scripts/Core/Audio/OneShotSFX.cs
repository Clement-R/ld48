using Cake.Opera;

namespace Game
{
    public class OneShotSFX : AOneShotSFX
    {
        protected override ISoundSystem GetSoundSystem()
        {
            return SoundsManager.Instance;
        }
    }
}