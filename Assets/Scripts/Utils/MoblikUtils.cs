using System.Collections.Generic;
using UnityEngine;

public static class MoblikUtils
{
    //#if UNITY_EDITOR
    //[UnityEditor.MenuItem("Moblik/Test")]
    //public static void Test()
    //{
    //    Debug.Log("Teste");
    //}

    //[UnityEditor.MenuItem("Moblik/Test2 %g")]
    //public static void Test2()
    //{
    //    Debug.Log("Teste2");
    //}
    //#endif

    /// <summary>
    /// Define uma escala uniforme para o <see cref="Transform"/>.
    /// </summary>
    /// <param name="transform">O Transform cuja escala será alterada.</param>
    /// <param name="newSize">O novo valor da escala aplicado igualmente aos eixos X, Y e Z.</param>
    public static void ChangeScale(this Transform transform, float newSize)
    {
        transform.localScale = Vector3.one * newSize;
    }

    /// <summary>
    /// Aplica movimento ao personagem que possuir um <see cref="Rigidbody"/>.
    /// Os inputs usados são os de legado da Unity.
    /// </summary>
    /// <param name="rigidbody">O Rigidbody anexado no personagem.</param>
    /// <param name="speed">Variável que determinará a velocidade do personagem.</param>
    public static void MovementWithLegacyInput(this Rigidbody rigidbody, float speed)
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        rigidbody.velocity = new Vector3(input.x, rigidbody.velocity.y, input.y);
    }

    public static void JumpWithLegacyInput(this Rigidbody rigidbody, float jumpForce)
    {
        if (Input.GetButtonDown("Jump"))
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpForce, rigidbody.velocity.z);
        }
    }

    #region RANDOM
    /// <summary>
    /// Obtém um elemento aleatório da lista.
    /// </summary>
    /// <typeparam name="T">Tipo dos elementos da lista.</typeparam>
    /// <param name="list">Lista de origem.</param>
    /// <returns>Um elemento aleatório ou <c>default(T)</c> se a lista estiver vazia.</returns>
    public static T GetRandomElement<T>(this List<T> list)
    {
        if (list.Count == 0) return default;

        return list[Random.Range(0, list.Count)];
    }

    /// <summary>
    /// Obtém um elemento aleatório do array.
    /// </summary>
    /// <typeparam name="T">Tipo dos elementos do array.</typeparam>
    /// <param name="array">Array de origem.</param>
    /// <returns>Um elemento aleatório ou <c>default(T)</c> se o array estiver vazio.</returns>
    public static T GetRandomElement<T>(this T[] array)
    {
        if (array.Length == 0) return default;

        return array[Random.Range(0, array.Length)];
    }

    /// <summary>
    /// Retorna um elemento aleatório da lista diferente do elemento informado.
    /// </summary>
    /// <typeparam name="T">Tipo dos elementos da lista.</typeparam>
    /// <param name="list">Lista da qual será selecionado um elemento.</param>
    /// <param name="unique">Elemento que não deve ser retornado, quando possível.</param>
    /// <returns>
    /// Um elemento aleatório diferente de <paramref name="unique"/>.
    /// Se a lista possuir apenas um elemento, esse elemento será retornado.
    /// </returns>
    public static T GetRandomElementAsUnique<T>(this List<T> list, T unique)
    {
        if (list.Count == 0) return default;
        if (list.Count == 1) return list[0];

        T random;

        do
        {
            random = list[Random.Range(0, list.Count)];
        }
        while (EqualityComparer<T>.Default.Equals(random, unique));

        return random;
    }

    /// <summary>
    /// Retorna um elemento aleatório do array diferente do elemento informado.
    /// </summary>
    /// <typeparam name="T">Tipo dos elementos do array.</typeparam>
    /// <param name="list">Array do qual será selecionado um elemento.</param>
    /// <param name="unique">Elemento que não deve ser retornado, quando possível.</param>
    /// <returns>
    /// Um elemento aleatório diferente de <paramref name="unique"/>.
    /// Se o array possuir apenas um elemento, esse elemento será retornado.
    /// </returns>
    public static T GetRandomElementAsUnique<T>(this T[] array, T unique)
    {
        if (array.Length == 0) return default;
        if (array.Length == 1) return array[0];

        T random;

        do
        {
            random = array[Random.Range(0, array.Length)];
        }
        while (EqualityComparer<T>.Default.Equals(random, unique));

        return random;
    }
    #endregion
}