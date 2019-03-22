using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using NUnit.Framework;

namespace Orleankka.Features
{
    namespace Background_jobs
    {
        using Utility;
        using Testing;

        [TestFixture]
        [RequiresSilo]
        public class Tests
        {
            BackgroundJobService jobs;

            [SetUp]
            public void SetUp()
            {
                jobs = new BackgroundJobService();
            }

            [Test]
            public void When_no_job_is_running()
            {
                Assert.That(jobs.IsRunning("foo"), Is.False);
                Assert.That(jobs["foo"], Is.Null);
                Assert.That(jobs.Count,  Is.EqualTo(0));
            }

            [Test]
            public async Task When_job_is_running()
            {
                var cancellation = new CancellationTokenSource();
                
                async Task Work(BackgroundJobTerminationToken _)
                {
                    while (!cancellation.IsCancellationRequested)
                        await Task.Delay(TimeSpan.FromMilliseconds(100), cancellation.Token);
                }

                var task = Task.Run(() => jobs.Run("foo", Work), cancellation.Token);

                Assert.That(jobs.IsRunning("foo"), Is.True);
                Assert.That(jobs["foo"], Is.Not.Null);
                Assert.That(jobs.Count,  Is.EqualTo(1));

                var job = jobs["foo"];
                Assert.That(job.IsRunning, Is.True);
                Assert.That(job.IsTerminated, Is.False);
                Assert.That(job.IsTerminationRequested, Is.False);

                cancellation.Cancel();
                await task;
            }
        }

        public class BackgroundJobService
        {
            readonly Dictionary<string, BackgroundJob> jobs = 
                 new Dictionary<string, BackgroundJob>();

            public int Count => jobs.Count;
            public bool IsRunning(string id) => jobs.ContainsKey(id);
            public BackgroundJob this[string id] => jobs.Find(id);

            public BackgroundJob Run(string id, Func<BackgroundJobTerminationToken, Task> job)
            {
                var j = new BackgroundJob(job);
                jobs.Add(id, j);
                j.Run();
                return j;
            }
        }

        public class BackgroundJob
        {
            readonly Func<BackgroundJobTerminationToken, Task> job;

            internal BackgroundJob(Func<BackgroundJobTerminationToken, Task> job) => 
                this.job = job;

            public bool IsRunning()
            {
                return !IsTerminated();
            }

            public bool IsTerminated()
            {
                return false;
            }

            public bool IsTerminationRequested()
            {
                return false;
            }

            public void Run()
            {
                throw new NotImplementedException();
            }
        }

        public class BackgroundJobTerminationToken
        {

        }
    }
}
