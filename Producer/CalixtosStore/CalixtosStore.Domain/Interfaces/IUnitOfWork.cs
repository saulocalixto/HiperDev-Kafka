﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CalixtosStore.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}