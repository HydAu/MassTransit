// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.ServiceBus.Internal
{
	using System;
	using Saga;
	using Subscriptions;

	public class SagaSubscription<TSaga, TMessage> :
		ISubscriptionTypeInfo
		where TSaga : class, ISaga, Consumes<TMessage>.All
        where TMessage : class, CorrelatedBy<Guid>
	{
		public void Dispose()
		{
		}

		public void Subscribe<T>(IDispatcherContext context, T component) where T : class
		{
			throw new NotImplementedException();
		}

		public void Unsubscribe<T>(IDispatcherContext context, T component) where T : class
		{
			throw new NotImplementedException();
		}

		public void AddComponent(IDispatcherContext context)
		{
			GetSagaDispatcher(context);
		}

		public void RemoveComponent(IDispatcherContext context)
		{
			throw new NotImplementedException("Components cannot be removed");
		}

		public void GetSagaDispatcher(IDispatcherContext context)
		{
			IMessageDispatcher<TMessage> messageDispatcher = context.GetDispatcher<MessageDispatcher<TMessage>>();

			SagaDispatcher<TSaga, TMessage> dispatcher = messageDispatcher.GetDispatcher<SagaDispatcher<TSaga, TMessage>>();

			if (dispatcher == null)
			{
				ISagaRepository<TSaga> repository = context.Builder.Build<ISagaRepository<TSaga>>();

				dispatcher = new SagaDispatcher<TSaga, TMessage>(repository);

				context.Attach(dispatcher);
				context.AddSubscription(new Subscription(typeof (TMessage).FullName, context.Bus.Endpoint.Uri));
			}
		}
	}
    
    public class StartSagaSubscription<TSaga, TMessage> :
		ISubscriptionTypeInfo
		where TSaga : class, ISaga, Consumes<TMessage>.All
        where TMessage : class, CorrelatedBy<Guid>
	{
		public void Dispose()
		{
		}

		public void Subscribe<T>(IDispatcherContext context, T component) where T : class
		{
			throw new NotImplementedException();
		}

		public void Unsubscribe<T>(IDispatcherContext context, T component) where T : class
		{
			throw new NotImplementedException();
		}

		public void AddComponent(IDispatcherContext context)
		{
			GetSagaDispatcher(context);
		}

		public void RemoveComponent(IDispatcherContext context)
		{
			throw new NotImplementedException("Components cannot be removed");
		}

		public void GetSagaDispatcher(IDispatcherContext context)
		{
			IMessageDispatcher<TMessage> messageDispatcher = context.GetDispatcher<MessageDispatcher<TMessage>>();

			StartSagaDispatcher<TSaga, TMessage> dispatcher = messageDispatcher.GetDispatcher<StartSagaDispatcher<TSaga, TMessage>>();

			if (dispatcher == null)
			{
				ISagaRepository<TSaga> repository = context.Builder.Build<ISagaRepository<TSaga>>();

				dispatcher = new StartSagaDispatcher<TSaga, TMessage>(repository);

				context.Attach(dispatcher);
				context.AddSubscription(new Subscription(typeof (TMessage).FullName, context.Bus.Endpoint.Uri));
			}
		}
	}
}