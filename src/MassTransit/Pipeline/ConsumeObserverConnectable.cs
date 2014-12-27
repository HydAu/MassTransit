// Copyright 2007-2014 Chris Patterson, Dru Sellers, Travis Smith, et. al.
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
namespace MassTransit.Pipeline
{
    using System;
    using System.Threading.Tasks;
    using Util;


    /// <summary>
    /// Observes the consumption of a message regardless of actual type
    /// </summary>
    public class ConsumeObserverConnectable :
        Connectable<IConsumeObserver>,
        IConsumeObserver
    {
        public Task PreDispatch<T>(ConsumeContext<T> context)
            where T : class
        {
            return ForEach(x => x.PreDispatch(context));
        }

        public Task PostDispatch<T>(ConsumeContext<T> context)
            where T : class
        {
            return ForEach(x => x.PostDispatch(context));
        }

        public Task DispatchFault<T>(ConsumeContext<T> context, Exception exception)
            where T : class
        {
            return ForEach(x => x.DispatchFault(context, exception));
        }
    }
}