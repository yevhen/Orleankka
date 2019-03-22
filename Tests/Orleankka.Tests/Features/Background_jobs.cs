using NUnit.Framework;

namespace Orleankka.Features
{
    namespace Background_jobs
    {
        using Testing;

        [TestFixture]
        [RequiresSilo]
        public class Tests
        {
            [Test]
            public void When_no_job_is_running()
            {
                var jobs = new BackgroundJobService();
                Assert.That(jobs.IsRunning("foo"), Is.False);
                Assert.That(jobs["foo"], Is.Null);
                Assert.That(jobs.Count,  Is.EqualTo(0));
            }
        }

        public class BackgroundJobService
        {
            public int Count { get; }

            public bool IsRunning(string id)
            {
                return false;
            }

            public BackgroundJob this[string id]
            {
                get { return null; }
            }
        }

        public class BackgroundJob
        {

        }
    }
}
