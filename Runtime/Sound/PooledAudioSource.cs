using System;
using StansAssets.Foundation.Async;
using StansAssets.Foundation.Extensions;
using UnityEngine;

namespace StansAssets.Foundation.Audio
{
    /// <summary>
    /// A pooled wrapper around Unity <see cref="AudioSource"/>.
    /// Used as audio source for the <see cref="AudioCenter"/>.
    /// You can also obtain a new instance by 
    /// </summary>
    public class PooledAudioSource
    {
        readonly AudioSource m_AudioSource;
        readonly GameObject m_GameObject;
        internal event Action OnRelease;

        internal PooledAudioSource(Transform root)
        {
            m_GameObject = new GameObject(nameof(AudioSource));
            m_GameObject.transform.parent = root;
            m_GameObject.transform.Reset();

            m_AudioSource = m_GameObject.AddComponent<AudioSource>();
            m_AudioSource.playOnAwake = false;
        }

        public void PlayOneShot(AudioClip clip)
        {
            m_GameObject.name = clip.name;
            m_AudioSource.PlayOneShot(clip);
            CoroutineUtility.WaitForSeconds(clip.length, Release);
        }

        /// <summary>
        /// Release pooled sound.
        /// </summary>
        public void Release()
        {
            OnRelease?.Invoke();
        }

        internal void Deactivate()
        {
            m_GameObject.SetActive(false);
            m_AudioSource.volume = 1;
            m_AudioSource.clip = null;
        }

        internal void Activate()
        {
            m_GameObject.SetActive(true);
        }
    }
}
