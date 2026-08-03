using System;
using System.Collections.Generic;
using YG;

public static class Ads
{
    private static Dictionary<string, Action> rewards = new();

    public static void Init()
    {
        YG2.onRewardAdv += OnRewarded;
    }

    public static void ShowReward(string id, Action callback)
    {
        rewards[id] = callback;
        YG2.RewardedAdvShow(id);
    }

    private static void OnRewarded(string id)
    {
        if (rewards.ContainsKey(id))
        {
            rewards[id]?.Invoke();
            rewards.Remove(id);
        }
    }
}
