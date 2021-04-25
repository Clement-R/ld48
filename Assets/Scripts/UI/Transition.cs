using System.Threading.Tasks;

using UnityEngine;

namespace Game
{
    public abstract class Transition : MonoBehaviour
    {
        public abstract Task Show();
        public abstract Task Hide();
    }
}