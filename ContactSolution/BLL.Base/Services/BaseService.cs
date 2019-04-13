using System;
using Contracts.BLL.Base.Services;

namespace BLL.Base.Services
{
    public class BaseService : IBaseService
    {
        private readonly Guid _instanceId = Guid.NewGuid();
        public Guid InstanceId => _instanceId;
    }
}   