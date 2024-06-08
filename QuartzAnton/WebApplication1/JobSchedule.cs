namespace WebApplication1
{
    public class JobSchedule
    {
        public JobSchedule(Type jobtype, string cronExpression)
        {
            JobType = jobtype;
            CronExpression = cronExpression;
        }
        public Type JobType { get; }
        public string CronExpression { get; }

    }
}
