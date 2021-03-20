using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.AdminModels;
using System;

public class PlayFabReward : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void GetReward()
    {
        GetTasksRequest request = new GetTasksRequest();
        NameIdentifier nameTask = new NameIdentifier();
        nameTask.Id = "2AEB71BF37A7A38E";
        nameTask.Name = "Reward";
        request.Identifier = nameTask;
        PlayFabAdminAPI.GetTasks(request, OnGetReward, error => { Debug.Log(error.GenerateErrorReport()); });
    }

    private void OnGetReward(GetTasksResult result)
    {
        List<ScheduledTask> tasks = result.Tasks;
        foreach (ScheduledTask task in tasks)
        {
           Debug.Log(task.NextRunTime.Value.ToLongDateString());
           GameLoader.gameLoader.isScheduleTask = true;
            Register.register.nextRewardTime = task.NextRunTime.Value.ToShortTimeString();
           if (PlayFabAuth.playFabAuth.hasReward)
            {
                Register.register.nextReward.text = "Get the Rewrad Now !!";
            }
           else
            {
                Register.register.nextReward.text = task.NextRunTime.Value.ToShortTimeString();
            }
           
           return;
        }
    }
}
