namespace Application.Services.Helpers.Definition
{
    public abstract class StatisticService
    {
        public void FillStats()
        {
            GetInfo();
            CalcStats();
            SaveStats();
        }


        protected abstract void GetInfo();

        protected abstract void CalcStats();

        protected abstract void SaveStats();

    }
}
