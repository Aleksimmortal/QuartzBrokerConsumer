
using Quartz;
using Quartz.Spi;

namespace WebApplication1
{
    public class QuartsHostedServices : IHostedService
    {
        private readonly ISchedulerFactory _shedulerFactory;
        private readonly IJobFactory _jobfactory;
        private readonly IEnumerable<JobSchedule> _jobSchedules;

        public QuartsHostedServices(
            ISchedulerFactory shedulerFactory,
            IJobFactory jobfactory,
            IEnumerable<JobSchedule> jobSchedules)
        {
            _shedulerFactory = shedulerFactory;
            _jobfactory = jobfactory;
            _jobSchedules = jobSchedules;
        }
        public IScheduler Scheduler { get; set; }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _shedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobfactory;

            foreach (var jobSchedule in _jobSchedules)
            {
                var job = CreateJob(jobSchedule);
                var trigger = CreateTrigger(jobSchedule);

                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }

            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        private static IJobDetail CreateJob(JobSchedule schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();
        }

        private static ITrigger CreateTrigger(JobSchedule schedule)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.JobType.FullName}.trigger")
                .WithCronSchedule(schedule.CronExpression)
                .WithDescription(schedule.CronExpression)
                .Build();
        }
    }
}
