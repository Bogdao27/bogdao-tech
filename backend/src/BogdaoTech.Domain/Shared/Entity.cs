using System;
using System.Collections.Generic;
using System.Text;

namespace BogdaoTech.Domain.Shared
{
    //Общий абстрактный класс для всех айдишников
    public abstract class Entity<TId> where TId : notnull
    {
        protected Entity(TId id) => Id = id;

        public TId Id { get; }
    }
}
