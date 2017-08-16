using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace YwkManage.OA.Model
{
    public static class OAContextFactory
    {
        private static OAContext _oaContext;
        public static OAContext GetDbContext()
        {
            _oaContext =(OAContext) CallContext.GetData("OAContext");
            if (_oaContext==null)
            {
                _oaContext = new OAContext();
                CallContext.SetData("OAContext", _oaContext);
            }
            return _oaContext; 
        }
    }
}
