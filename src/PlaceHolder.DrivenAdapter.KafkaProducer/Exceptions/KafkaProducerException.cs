using PlaceHolder.Domain.SeedWork.Exceptions.Base;
using System;
using System.Runtime.Serialization;

namespace PlaceHolder.DrivenAdapter.KafkaProducer.Exceptions
{
    internal class KafkaProducerException : BaseException
    {
        public KafkaProducerException(string message) : base(message) { }
    }
}