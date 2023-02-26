namespace Assets
{
    using UnityEngine;

    public interface IGenerateGameObject
    {
        GameObject Build<T>(Transform placeholder, T instance);
    }
}
