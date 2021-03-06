﻿using System;
using Quartz.Impl;
using IDPJobManager.Web;
using IDPJobManager.Core.SchedulerProviders;
using IDPJobManager.Web.Configuration;
using IDPJobManager.Core;
using Quartz;
using IDPJobManager.Jobs;

namespace IDPJobManager
{
    class Program
    {
        static void Main(string[] args)
        {
            ////1.首先创建一个作业调度池
            //var properties = new NameValueCollection();
            ////存储类型
            //properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            ////表明前缀
            //properties["quartz.jobStore.tablePrefix"] = "QRTZ_";
            ////驱动类型
            //properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz";                //数据源名称
            //properties["quartz.jobStore.dataSource"] = "myDS";
            ////连接字符串
            //properties["quartz.dataSource.myDS.connectionString"] = @"Data Source=DESKTOP-DCN8TM6\SQLEXPRESS;Initial Catalog=IDP_Jobs;User ID=sa;Password=123456;";
            ////sqlserver版本
            //properties["quartz.dataSource.myDS.provider"] = "SqlServer-20";
            ////最大链接数
            ////properties["quartz.dataSource.myDS.maxConnections"] = "5";
            //// First we must get a reference to a scheduler
            //var schedulerFactory = new StdSchedulerFactory(properties);
            //var scheduler = schedulerFactory.GetScheduler();
            

            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler();
            var jobTrigger = TriggerBuilder.Create().WithSimpleSchedule(x => x.WithIntervalInSeconds(10).WithRepeatCount(int.MaxValue)).Build();
            var jobKey = JobKey.Create("job1", "jobgroup1");
            IJobDetail jobDetail = scheduler.GetJobDetail(jobKey);
            if (jobDetail == null)
            {
                jobDetail = JobBuilder.Create<HelloJob>().WithIdentity(jobKey).Build();
                scheduler.ScheduleJob(jobDetail, jobTrigger);
            }
            else
            {
                scheduler.ResumeJob(jobKey);
            }

            using (IDPJobManagerStarter.Configure.UsingScheduler(scheduler)
                    .HostedOnDefault()
                    .Start())
            {
                scheduler.Start();
                Console.Read();
            }

            Console.ReadKey();
        }
    }
}
