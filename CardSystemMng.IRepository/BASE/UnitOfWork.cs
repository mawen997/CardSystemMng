using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardSystemMng.IRepository.BASE
{
    public interface IUnitOfWork
    {
        SqlSugarClient GetDbClient();

        void BeginTran();

        void CommitTran();
        void RollbackTran();
    }
}
