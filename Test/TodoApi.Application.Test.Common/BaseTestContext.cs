using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq.AutoMock;
using Shouldly;
using TodoApi.DataLayer.DataAccess;

namespace TodoApi.Application.Test.Common
{
    public abstract class BaseTestContext<TSubject, TContext>
        where TSubject : class
        where TContext : BaseTestContext<TSubject, TContext>
    {
        protected readonly AutoMocker Mock;

        protected readonly List<Action> Assertions = new List<Action>();

        public BaseTestContext()
        {
            Mock = new AutoMocker();
        }

        public TSubject CreateSubject()
        {
            return Mock.CreateInstance<TSubject>();
        }

        public TContext GivenCommitter()
        {
            Mock.Setup<ICommitter, Task>(x => x.Commit()).Returns(Task.CompletedTask).Verifiable();

            return this as TContext;
        }

        public TContext VerifyScenario()
        {
            foreach (var assertion in Assertions)
            {
                assertion();
            }

            Mock.VerifyAll();

            return this as TContext;
        }

        public TContext VerifyResultIsNotNull(object result)
        {
            Assertions.Add(() => result.ShouldNotBeNull());

            return this as TContext;
        }

        public TContext Verify(Action assertion)
        {
            Assertions.Add(assertion);

            return this as TContext;
        }
    }
}