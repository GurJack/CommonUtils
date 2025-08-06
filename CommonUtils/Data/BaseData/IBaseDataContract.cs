using System;
using System.Collections.Generic;

namespace BaseData
{
    public interface IBaseDataContract
    {
        void Delete(BaseModel item);
        object GetData();
        Type GetDataType();
        void Insert(BaseModel item);
        void Update(BaseModel item);
        BaseModel GetRecord(Guid id);
    }
}