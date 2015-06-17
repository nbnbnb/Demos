﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace CustomChannel
{
    public class SimpleSessionBindingElement : BindingElement
    {
        protected void Print(string methodName)
        {
            Console.WriteLine("{0}.{1}", this.GetType().Name, methodName);
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            this.Print("BuildChannelFactory()");
            return new SimpleSessionChannelFactory<TChannel>(context);
        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            this.Print("BuildChannelListener()");
            return new SimpleSessionChannelListener<TChannel>(context);
        }

        public override BindingElement Clone()
        {
            return new SimpleSessionBindingElement();
        }

        public override T GetProperty<T>(BindingContext context)
        {
            return context.GetInnerProperty<T>();
        }
    }
}