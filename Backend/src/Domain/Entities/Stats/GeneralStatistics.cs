﻿namespace Domain.Entities.Stats
{
    public enum StatisticsTarget
    {
        Lessons,
        Groups,
        Teachers,
    }

    public class GeneralStatistics : BaseEntity
    {
        public StatisticsTarget Target { get; set; }

        public double TotalAmount { get; set; }

        public DateTime RecalcDate { get; set; }


    }
}
