using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private Dictionary<string, Coroutine> activeTimers = new Dictionary<string, Coroutine>();

    /// <summary>
    /// Checks whether a timer with the given id is currently active.
    /// </summary>
    /// <param name="timerId">The unique id of the timer being checked</param>
    /// <returns>Returns true if the timer is running, otherwise returns false</returns>
    public bool isRunning(string timerId)
    {
        return activeTimers.ContainsKey(timerId);
    }

    /// <summary>
    /// Starts a timer and runs the completion action after the delay. If a timer with the same id already exists, it is restarted.
    /// </summary>
    /// <param name="timerId">The unique id used to track the timer</param>
    /// <param name="delaySeconds">The amount of time in seconds before the timer finishes</param>
    /// <param name="onComplete">The method that runs after the timer finishes</param>
    public void startTimer(string timerId, float delaySeconds, Action onComplete)
    {
        if (string.IsNullOrWhiteSpace(timerId))
        {
            Debug.LogWarning("Timer id cannot be empty.");
            return;
        }

        cancelTimer(timerId);
        activeTimers[timerId] = StartCoroutine(runTimer(timerId, delaySeconds, onComplete));
    }

    /// <summary>
    /// Starts a timer only if another timer with the same id is not already running.
    /// </summary>
    /// <param name="timerId">The unique id used to track the timer</param>
    /// <param name="delaySeconds">The amount of time in seconds before the timer finishes</param>
    /// <param name="onComplete">The method that runs after the timer finishes</param>
    public void startTimerIfNotRunning(string timerId, float delaySeconds, Action onComplete)
    {
        if (isRunning(timerId))
        {
            return;
        }

        startTimer(timerId, delaySeconds, onComplete);
    }

    /// <summary>
    /// Stops a timer with the matching id if it exists.
    /// </summary>
    /// <param name="timerId">The unique id of the timer to cancel</param>
    public void cancelTimer(string timerId)
    {
        if (activeTimers.TryGetValue(timerId, out Coroutine timer))
        {
            StopCoroutine(timer);
            activeTimers.Remove(timerId);
        }
    }

    /// <summary>
    /// Stops every active timer currently being tracked by this component.
    /// </summary>
    public void cancelAllTimers()
    {
        foreach (Coroutine timer in activeTimers.Values)
        {
            StopCoroutine(timer);
        }

        activeTimers.Clear();
    }

    /// <summary>
    /// Internal coroutine that waits for the delay and then runs the completion action.
    /// </summary>
    /// <param name="timerId">The unique id of the timer being processed</param>
    /// <param name="delaySeconds">The amount of time in seconds before the timer finishes</param>
    /// <param name="onComplete">The method that runs after the timer finishes</param>
    /// <returns>Waits for the timer duration before completing the timer</returns>
    private IEnumerator runTimer(string timerId, float delaySeconds, Action onComplete)
    {
        yield return new WaitForSeconds(delaySeconds);

        activeTimers.Remove(timerId);
        onComplete?.Invoke();
    }
}