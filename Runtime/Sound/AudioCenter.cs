using System;
using StansAssets.Foundation.Extensions;
using StansAssets.Foundation.Patterns;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StansAssets.Foundation.Audio
{
    /// <summary>
    /// Simple audio manager that allow to play 2D pooled sounds.
    /// Common use case is for playing lot's of UI sounds or other game interaction sounds without 3D effect.
    /// </summary>
    public class AudioCenter
    {
        readonly GameObject m_Root;
        readonly ObjectPool<PooledAudioSource> m_Pool;

        /// <summary>
        ///  Creates new audio center.
        /// </summary>
        /// <param name="name">Name will be used for the gameobject that will be used as parent for the polled audio sources.</param>
        /// <param name="audioListener">The new audio center instance will be parented to the audioListener gameobject. </param>
        public AudioCenter(string name, AudioListener audioListener)
            : this(name)
        {
            m_Root.transform.parent = audioListener.transform;
            m_Root.transform.Reset();
        }

        /// <summary>
        /// Creates new audio center.
        /// </summary>
        /// <param name="name">Name will be used for the gameobject that will be used as parent for the polled audio sources.</param>
        public AudioCenter(string name)
        {
            m_Root = new GameObject(name);
            Object.DontDestroyOnLoad(m_Root);
            m_Pool = new ObjectPool<PooledAudioSource>(() =>
            {
                var pooledSound = new PooledAudioSource(m_Root.transform);
                pooledSound.OnRelease += () =>
                {
                    m_Pool?.Release(pooledSound);
                };

                return pooledSound;
            }, audioSource =>
            {
                audioSource.Activate();
            }, audioSource =>
            {
                audioSource.Deactivate();
            });
        }


        /// <summary>
        /// Will take avaliable pooled audio source to play clip once.
        /// The pooled audio source will be released after clip is played.
        /// </summary>
        /// <param name="clip">The audio clip to play.</param>
        public void PlayOneShot(AudioClip clip)
        {
            m_Pool.Get().PlayOneShot(clip);
        }

        /// <summary>
        /// Get instance of the pooled audio source.
        /// You will be responsible for releasing this audio source once you are done using it.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public PooledAudioSource GetAudioSource()
        {
            return m_Pool.Get();
        }
    }
}
