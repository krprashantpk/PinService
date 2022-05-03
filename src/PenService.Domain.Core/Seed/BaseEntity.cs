using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Domain.Core.Seed
{
    public abstract class BaseEntity
    {

        List<INotification>? _events;

        public void AddEvent(INotification @event)
        {
            if (_events == null) _events = new List<INotification>();
            if (_events.Contains(@event)) throw new ArgumentException($"{@event} is already added.", "@event");
            _events.Add(@event);
        }

        public void RemoveEvent(INotification @event)
        {
            if (_events?.Contains(@event) == true)
            {
                _events.Remove(@event);
            }
        }

    }
}
