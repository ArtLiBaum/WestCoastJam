using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSetManager : MonoBehaviour
{
    List<PropSet> EventQueue = new List<PropSet>();
    List<PropSet> UsedEvents = new List<PropSet>();

    int totalWeight = 0;

    RandomSetManager.PriorityCompare Compare = new PriorityCompare();

    class PriorityCompare : IComparer<PropSet>
    {
        public int Compare(PropSet x, PropSet y)
        {
            if (x.Weight == y.Weight)
                return 0;
            if (x.Weight > y.Weight)
                return 1;
            else
                return -1;
        }
    }

    public virtual void Start()
    {
        totalWeight = 0;
        foreach (PropSet e in EventQueue)
        {
            totalWeight += e.Weight;
        }
    }

    public void SortList()
    {
        EventQueue.Sort(Compare);
    }

    public void AddEvent(PropSet e)
    {
        EventQueue.Add(e);
        totalWeight += e.Weight;
    }

    public void RemoveEvent(PropSet e)
    {
        EventQueue.Remove(e);
        UsedEvents.Add(e);
        //Re-add all the events if empty
        if (EventQueue.Count == 0)
        {
            totalWeight = 0;
            foreach (PropSet v in UsedEvents)
            {
                AddEvent(v);
            }

            UsedEvents.Clear();
            SortList();
        }
        else
            totalWeight -= e.Weight;
    }

    public PropSet SelectSet(bool removeWhenDone = true)
    {
        SortList();

        PropSet selectedEvent = null;
        int randomNumber = Random.Range(0, totalWeight);

        foreach (PropSet e in EventQueue)
        {
            if (randomNumber < e.Weight)
            {
                selectedEvent = e;
                break;
            }

            randomNumber -= e.Weight;
        }

        if (selectedEvent != null && removeWhenDone)
        {
            RemoveEvent(selectedEvent);
        }

        return selectedEvent;
    }
}