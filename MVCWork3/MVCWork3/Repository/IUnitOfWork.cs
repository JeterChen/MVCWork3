using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCWork3.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// context
        /// </summary>
        DbContext Context { get; set; }
        /// <summary>
        /// save change
        /// </summary>
        void Commit();


    }
}
